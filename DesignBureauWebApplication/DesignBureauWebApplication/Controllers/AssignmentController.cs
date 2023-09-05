using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesignBureauWebApplication.Repository;
using DesignBureauWebApplication.ViewModels;
using System.ComponentModel.DataAnnotations;
using DesignBureauWebApplication.Data.Enum;

namespace DesignBureauWebApplication.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAssignmentDictionaryRepository _assignmentDictionaryRepository;
        private readonly IAssignmentHistoryRepository _assignmentHistoryRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IExecutionRepository _executionRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public AssignmentController(IAssignmentRepository assignmentRepository,
            IAssignmentHistoryRepository assignmentHistoryRepository,
            IWorkRepository workRepository, IAssignmentDictionaryRepository assignmentDictionaryRepository,
            IExecutionRepository executionRepository, IWorkHistoryRepository workHistoryRepository)
        {
            _assignmentRepository = assignmentRepository;
            _assignmentHistoryRepository = assignmentHistoryRepository;
            _workRepository = workRepository;
            _assignmentDictionaryRepository = assignmentDictionaryRepository;
            _executionRepository = executionRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IActionResult> Details(int id)
        {
            Assignment assignment = await _assignmentRepository.GetByIdPlusDataAsync(id);
            if (assignment == null)
            {
                return View("Error");
            }

            var lastAssignmentStatus = await _assignmentHistoryRepository.GetLastAssignmentStatus(id);
            var assignmentDictionary = await _assignmentDictionaryRepository.GetByIdAsync(assignment.AssignmentDictionaryId);
            var work = await _workRepository.GetByIdAsync(assignment.WorkId);

            var assignmentVM = new DetailsAssignmentViewModel
            {
                AssignmentId = assignment.AssignmentId,
                WorkId = work.WorkId,
                Work = work,
                AssignmentDictionaryId = assignment.AssignmentDictionaryId,
                AssignmentDictionary = assignmentDictionary,
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate,
                AssignmentHistories = assignment.AssignmentHistories,
                Consumptions = assignment.Consumptions,
                Executions = assignment.Executions,
                LastAssignmentStatus = lastAssignmentStatus
            };

            return View(assignmentVM);
        }

        public async Task<ActionResult> ChangeStatus(int id)
        {
            var assignment = await _assignmentRepository.GetByIdPlusDataAsync(id);
            if (assignment != null)
            {
                var lastAssignmentStatus = await _assignmentHistoryRepository.GetLastAssignmentStatus(id);
                var assignmentHistory = new AssignmentHistory { };
                if (lastAssignmentStatus == AssignmentStatus.Preparation)
                {
                    assignmentHistory = new AssignmentHistory
                    {
                        AssignmentId = id,
                        AssignmentStatus = AssignmentStatus.InProgress,
                        CreatedAt = DateTime.Now
                    };
                }
                else if (lastAssignmentStatus == AssignmentStatus.InProgress)
                {
                    assignmentHistory = new AssignmentHistory
                    {
                        AssignmentId = id,
                        AssignmentStatus = AssignmentStatus.Completed,
                        CreatedAt = DateTime.Now
                    };
                }
                _assignmentHistoryRepository.Add(assignmentHistory);
            }
            return RedirectToAction("Details", new { id });
        }

        public async Task<ActionResult> Cancel(int id)
        {
            var assignment = await _assignmentRepository.GetByIdPlusDataAsync(id);
            var workId = assignment.WorkId;
            if (assignment != null)
            {
                if (assignment.Consumptions.Count == 0)
                {
                    var assignmentHistory = await _assignmentHistoryRepository.GetAll();
                    var assignmentHistoryToDelete = assignmentHistory.Where(ah => ah.AssignmentId == id);
                    foreach (var ah in assignmentHistoryToDelete)
                    {
                        _assignmentHistoryRepository.Delete(ah);
                    }
                    _assignmentRepository.Delete(assignment);

                    return RedirectToAction("Details", "Work", new { id = workId });
                }
                else
                {
                    var assignmentHistory = new AssignmentHistory
                    {
                        AssignmentId = id,
                        AssignmentStatus = AssignmentStatus.Cancelled,
                        CreatedAt = DateTime.Now
                    };
                    _assignmentHistoryRepository.Add(assignmentHistory);
                }
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int workId)
        {
            var assignmentDictionaries = await _assignmentDictionaryRepository.GetAll();
            var work = await _workRepository.GetByIdAsync(workId);
            if (work == null)
            {
                return NotFound();
            }

            var assignmentVM = new AssignmentViewModel
            {
                WorkId = workId,
                Work = work,
                AssignmentDictionaryList = new SelectList(assignmentDictionaries, "AssignmentDictionaryId", "AssignmentTitle")
            };

            return View(assignmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentViewModel assignmentVM)
        {
            if (ModelState.IsValid)
            {
                var work = await _workRepository.GetByIdAsync(assignmentVM.WorkId);
                var assignmentDictionaries = await _assignmentDictionaryRepository.GetAll();

                if (assignmentVM.StartDate > assignmentVM.EndDate)
                {
                    ModelState.AddModelError("StartDate", "Дата начала не может быть позже даты окончания");
                    assignmentVM = new AssignmentViewModel
                    {
                        WorkId = work.WorkId,
                        Work = work,
                        AssignmentDictionaryList = new SelectList(assignmentDictionaries, "AssignmentDictionaryId", "AssignmentTitle")
                    };
                    return View(assignmentVM);
                }
                else if (assignmentVM.StartDate < work.PlanStartDate)
                {
                    ModelState.AddModelError("StartDate", "Дата начала выполнения задачи не может быть раньше даты начала работы");
                    assignmentVM = new AssignmentViewModel
                    {
                        WorkId = work.WorkId,
                        Work = work,
                        AssignmentDictionaryList = new SelectList(assignmentDictionaries, "AssignmentDictionaryId", "AssignmentTitle")
                    };
                    return View(assignmentVM);
                }
                else if (assignmentVM.StartDate > work.PlanOverDate)
                {
                    ModelState.AddModelError("StartDate", "Дата начала выполнения задачи не может быть позже даты окончания работы");
                    assignmentVM = new AssignmentViewModel
                    {
                        WorkId = work.WorkId,
                        Work = work,
                        AssignmentDictionaryList = new SelectList(assignmentDictionaries, "AssignmentDictionaryId", "AssignmentTitle")
                    };
                    return View(assignmentVM);
                }
                else if (assignmentVM.EndDate < work.PlanStartDate)
                {
                    ModelState.AddModelError("EndDate", "Дата окончания выполнения задачи не может быть раньше даты начала работы");
                    assignmentVM = new AssignmentViewModel
                    {
                        WorkId = work.WorkId,
                        Work = work,
                        AssignmentDictionaryList = new SelectList(assignmentDictionaries, "AssignmentDictionaryId", "AssignmentTitle")
                    };
                    return View(assignmentVM);
                }
                else if (assignmentVM.EndDate > work.PlanOverDate)
                {
                    ModelState.AddModelError("EndDate", "Дата окончания выполнения задачи не может быть позже даты окончания работы");
                    assignmentVM = new AssignmentViewModel
                    {
                        WorkId = work.WorkId,
                        Work = work,
                        AssignmentDictionaryList = new SelectList(assignmentDictionaries, "AssignmentDictionaryId", "AssignmentTitle")
                    };
                    return View(assignmentVM);
                }
                else
                {
                    if (assignmentVM.AssignmentDictionaryId == 0 && !string.IsNullOrEmpty(assignmentVM.NewAssignmentDictionary))
                    {
                        var newAssignmentDictionary = new AssignmentDictionary
                        {
                            AssignmentTitle = assignmentVM.NewAssignmentDictionary
                        };
                        _assignmentDictionaryRepository.Add(newAssignmentDictionary);

                        assignmentVM.AssignmentDictionaryId = newAssignmentDictionary.AssignmentDictionaryId;
                    }

                    var workStatus = await _workHistoryRepository.GetLastWorkStatus(work.WorkId);
                    if (work.Assignments.Count == 0 && workStatus == WorkStatus.Preparation)
                    {
                        var workHistory = new WorkHistory
                        {
                            WorkId = work.WorkId,
                            WorkStatus = WorkStatus.InProgress,
                            CreatedAt = DateTime.Now
                        };
                        _workHistoryRepository.Add(workHistory);
                    }

                    var assignment = new Assignment
                    {
                        AssignmentDictionaryId = assignmentVM.AssignmentDictionaryId,
                        WorkId = assignmentVM.WorkId,
                        StartDate = assignmentVM.StartDate,
                        EndDate = assignmentVM.EndDate
                    };
                    _assignmentRepository.Add(assignment);

                    var assignmentHistory = new AssignmentHistory
                    {
                        AssignmentId = assignment.AssignmentId,
                        AssignmentStatus = AssignmentStatus.Created,
                        CreatedAt = DateTime.Now
                    };
                    _assignmentHistoryRepository.Add(assignmentHistory);

                    return RedirectToAction("Details", "Work", new { id = assignmentVM.WorkId });
                }
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
                return View(assignmentVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);
            var work = assignment.Work;

            if (work == null)
            {
                return View("Error");
            }

            var assignmentVM = new AssignmentViewModel
            {
                AssignmentId = id,
                AssignmentDictionaryId = assignment.AssignmentDictionaryId,
                AssignmentDictionaryList = new SelectList(await _assignmentDictionaryRepository.GetAll(), "AssignmentDictionaryId", "AssignmentTitle"),
                WorkId = assignment.WorkId,
                Work = work,
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate
            };

            return View(assignmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AssignmentViewModel assignmentVM)
        {
            if (ModelState.IsValid)
            {
                var assignment = new Assignment
                {
                    AssignmentId= id,
                    AssignmentDictionaryId = assignmentVM.AssignmentDictionaryId,
                    WorkId = assignmentVM.WorkId,
                    StartDate = assignmentVM.StartDate,
                    EndDate = assignmentVM.EndDate
                };
                _assignmentRepository.Update(assignment);
            }
            else
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Error", assignmentVM);
            }

            return RedirectToAction("Details", "Work", new { id = assignmentVM.WorkId });
        }
    }
}
