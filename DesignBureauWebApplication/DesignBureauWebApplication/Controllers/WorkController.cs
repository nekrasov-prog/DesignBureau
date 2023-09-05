using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.ViewModels;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Evaluation;

namespace DesignBureauWebApplication.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkRepository _workRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkDictionaryRepository _workDictionaryRepository;
        private readonly IProjectHistoryRepository _projectHistoryRepository;

        public WorkController(IWorkRepository workRepository, IWorkHistoryRepository workHistoryRepository, IProjectRepository projectRepository, IWorkDictionaryRepository workDictionaryRepository,
            IProjectHistoryRepository projectHistoryRepository)
        {
            _workRepository = workRepository;
            _workHistoryRepository = workHistoryRepository;
            _projectRepository = projectRepository;
            _workDictionaryRepository = workDictionaryRepository;
            _projectHistoryRepository = projectHistoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Work> works = await _workRepository.GetAll();
            return View(works);
        }

        public async Task<IActionResult> Details(int id)
        {
            Work work = await _workRepository.GetByIdPlusDataAsync(id);
            if (work == null)
            {
                return View("Error");
            }
            var lastWorkStatus = await _workHistoryRepository.GetLastWorkStatus(id);
            var completedAssignments = await _workRepository.GetCompletedAssignmentsCountAsync(id);
            var totalAssignmentsCount = await _workRepository.GetTotalAssignmentsCount(id);
            var workDictionary = await _workDictionaryRepository.GetByIdAsync(work.WorkDictionaryId);
            var workVM = new DetailsWorkViewModel { };
            var project = await _projectRepository.GetByIdAsync(work.ProjectId);

            if (totalAssignmentsCount == 0)
            {
                workVM = new DetailsWorkViewModel
                {
                    WorkId = work.WorkId,
                    ProjectId = work.ProjectId,
                    PlanStartDate = work.PlanStartDate,
                    PlanOverDate = work.PlanOverDate,
                    WorkDictionaryId = work.WorkDictionaryId,
                    Assignments = work.Assignments,
                    WorkHistories = work.WorkHistories,
                    LastWorkStatus = lastWorkStatus,
                    WorkDictionary = workDictionary,
                    Project = project,
                    Progress = 0
                };
            }
            else
            {
                workVM = new DetailsWorkViewModel
                {
                    WorkId = work.WorkId,
                    ProjectId = work.ProjectId,
                    PlanStartDate = work.PlanStartDate,
                    PlanOverDate = work.PlanOverDate,
                    WorkDictionaryId = work.WorkDictionaryId,
                    Assignments = work.Assignments,
                    WorkHistories = work.WorkHistories,
                    LastWorkStatus = lastWorkStatus,
                    WorkDictionary = workDictionary,
                    Project = project,
                    Progress = (completedAssignments * 100) / totalAssignmentsCount
                };
            }

            return View(workVM);
        }

        public async Task<ActionResult> ChangeStatus(int id)
        {
            var work = await _workRepository.GetByIdPlusDataAsync(id);
            if (work != null)
            {
                var lastWorkStatus = await _workHistoryRepository.GetLastWorkStatus(id);
                var workHistory = new WorkHistory { };
                if (lastWorkStatus == WorkStatus.Preparation)
                {
                    workHistory = new WorkHistory
                    {
                        WorkId = id,
                        WorkStatus = WorkStatus.InProgress,
                        CreatedAt = DateTime.Now
                    };
                }
                else if (lastWorkStatus == WorkStatus.InProgress)
                {
                    workHistory = new WorkHistory
                    {
                        WorkId = id,
                        WorkStatus = WorkStatus.Completed,
                        CreatedAt = DateTime.Now
                    };
                }
                _workHistoryRepository.Add(workHistory);
            }
            return RedirectToAction("Details", new { id });
        }

        public async Task<ActionResult> Cancel(int id)
        {
            var work = await _workRepository.GetByIdPlusDataAsync(id);
            var projectId = work.ProjectId;
            if (work != null)
            {
                if (work.Assignments.Count == 0)
                {
                    var workHistory = await _workHistoryRepository.GetAll();
                    var workHistoryToDelete = workHistory.Where(wh => wh.WorkId == id);
                    foreach (var wh in workHistoryToDelete)
                    {
                        _workHistoryRepository.Delete(wh);
                    }
                    _workRepository.Delete(work);

                    return RedirectToAction("Details", "Work", new { id = projectId });
                }
                else
                {
                    var workHistory = new WorkHistory
                    {
                        WorkId = id,
                        WorkStatus = WorkStatus.Cancelled,
                        CreatedAt = DateTime.Now
                    };
                    _workHistoryRepository.Add(workHistory);
                }
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int projectId)
        {
            var workDictionaries = await _workRepository.GetAllWorkDictionaries();
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                // Если проект не найден, можно вернуть ошибку или выполнить другие действия
                return NotFound();
            }

            var workVM = new WorkViewModel
            {
                ProjectId = projectId,
                Project = project,
                WorkDictionaryList = new SelectList(workDictionaries, "WorkDictionaryId", "WorkTitle")
            };
            return View(workVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkViewModel workVM)
        {
            if (!ModelState.IsValid)
            {
                var project = await _projectRepository.GetByIdPlusDataAsync(workVM.ProjectId);
                var workDictionaries = await _workRepository.GetAllWorkDictionaries();
                
                if (workVM.PlanStartDate > workVM.PlanOverDate)
                {
                    ModelState.AddModelError("PlanStartDate", "Дата начала не может быть позже даты окончания");
                    workVM = new WorkViewModel
                    {
                        ProjectId = project.ProjectId,
                        Project = project,
                        WorkDictionaryList = new SelectList(workDictionaries, "WorkDictionaryId", "WorkTitle")
                    };
                    return View(workVM);
                }
                else if (workVM.PlanStartDate < project.PlanStartDate)
                {
                    ModelState.AddModelError("PlanStartDate", "Дата начала работы не может быть раньше даты начала проекта");
                    workVM = new WorkViewModel
                    {
                        ProjectId = project.ProjectId,
                        Project = project,
                        WorkDictionaryList = new SelectList(workDictionaries, "WorkDictionaryId", "WorkTitle")
                    };
                    return View(workVM);
                }
                else if (workVM.PlanStartDate > project.PlanOverDate)
                {
                    ModelState.AddModelError("PlanStartDate", "Дата начала работы не может быть позже даты окончания проекта");
                    workVM = new WorkViewModel
                    {
                        ProjectId = project.ProjectId,
                        Project = project,
                        WorkDictionaryList = new SelectList(workDictionaries, "WorkDictionaryId", "WorkTitle")
                    };
                    return View(workVM);
                }
                else if (workVM.PlanOverDate < project.PlanStartDate)
                {
                    ModelState.AddModelError("PlanOverDate", "Дата окончания работы не может быть раньше даты начала проекта");
                    workVM = new WorkViewModel
                    {
                        ProjectId = project.ProjectId,
                        Project = project,
                        WorkDictionaryList = new SelectList(workDictionaries, "WorkDictionaryId", "WorkTitle")
                    };
                    return View(workVM);
                }
                else if (workVM.PlanOverDate > project.PlanOverDate)
                {
                    ModelState.AddModelError("PlanOverDate", "Дата окончания работы не может быть позже даты окончания проекта");
                    workVM = new WorkViewModel
                    {
                        ProjectId = project.ProjectId,
                        Project = project,
                        WorkDictionaryList = new SelectList(workDictionaries, "WorkDictionaryId", "WorkTitle")
                    };
                    return View(workVM);
                }
                else
                {
                    if (workVM.WorkDictionaryId == 0 && !string.IsNullOrEmpty(workVM.NewWorkDictionary))
                    {
                        var newWorkDictionary = new WorkDictionary
                        {
                            WorkTitle = workVM.NewWorkDictionary
                        };
                        _workDictionaryRepository.Add(newWorkDictionary);

                        workVM.WorkDictionaryId = newWorkDictionary.WorkDictionaryId;
                    }

                    var projectStatus = await _projectHistoryRepository.GetLastProjectStatus(project.ProjectId);
                    if (project.Works.Count == 0 && projectStatus == ProjectStatus.Launch)
                    {
                        var projectHistory = new ProjectHistory
                        {
                            ProjectId= project.ProjectId,
                            ProjectStatus = ProjectStatus.Planning,
                            CreatedAt = DateTime.Now
                        };
                        _projectHistoryRepository.Add(projectHistory);
                    }

                    var work = new Work
                    {
                        ProjectId = workVM.ProjectId,
                        PlanStartDate = workVM.PlanStartDate,
                        PlanOverDate = workVM.PlanOverDate,
                        WorkDictionaryId = workVM.WorkDictionaryId
                    };
                    _workRepository.Add(work);

                    var workHistory = new WorkHistory
                    {
                        WorkId = work.WorkId,
                        WorkStatus = WorkStatus.Preparation,
                        CreatedAt = DateTime.Now
                    };
                    _workHistoryRepository.Add(workHistory);

                    return RedirectToAction("Details", "Project", new { id = workVM.ProjectId });
                }
            }
            ModelState.AddModelError("", "Проверьте правильность введенных данных");
            return View(workVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var work = await _workRepository.GetByIdAsync(id);
            var project = work.Project;

            if (work == null)
            {
                return View("Error");
            }
            var workVM = new WorkViewModel
            {
                WorkId = work.WorkId,
                PlanStartDate = work.PlanStartDate,
                PlanOverDate = work.PlanOverDate,
                ProjectId = work.ProjectId,
                Project = project,
                WorkDictionaryId = work.WorkDictionaryId,
                WorkDictionaryList = new SelectList(await _workRepository.GetAllWorkDictionaries(), "WorkDictionaryId", "WorkTitle")
            };
            return View(workVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, WorkViewModel workVM)
        {
            if (!ModelState.IsValid)
            {
                var work = new Work
                {
                    WorkId = id,
                    PlanStartDate = workVM.PlanStartDate,
                    PlanOverDate = workVM.PlanOverDate,
                    ProjectId = workVM.ProjectId,
                    WorkDictionaryId = workVM.WorkDictionaryId
                };
                _workRepository.Update(work);
            } else
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Error", workVM);
            }

            return RedirectToAction("Details", "Project", new { id = workVM.ProjectId });
        }
    }
}
