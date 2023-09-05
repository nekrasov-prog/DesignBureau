using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.ViewModels;
using DesignBureauWebApplication.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Data.Enum;
using Microsoft.AspNetCore.Authorization;

namespace DesignBureauWebApplication.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectHistoryRepository _projectHistoryRepository;

        public ProjectController(IProjectRepository projectRepository, IProjectHistoryRepository projectHistoryRepository)
        {
            _projectRepository = projectRepository;
            _projectHistoryRepository = projectHistoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Project> projects = await _projectRepository.GetAll();
            return View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            Project project = await _projectRepository.GetByIdPlusDataAsync(id);
            if (project == null)
            {
                return View("Error");
            }
            var lastProjectStatus = await _projectHistoryRepository.GetLastProjectStatus(id);
            var completedWorks = await _projectRepository.GetCompletedWorksCountAsync(id);
            var totalWorksCount = await _projectRepository.GetTotalWorksCount(id);
            var projectVM = new DetailsProjectViewModel { };
            if (totalWorksCount == 0)
            {
                projectVM = new DetailsProjectViewModel
                {
                    ProjectId = project.ProjectId,
                    ProjectTitle = project.ProjectTitle,
                    PlanStartDate = project.PlanStartDate,
                    PlanOverDate = project.PlanOverDate,
                    Works = project.Works,
                    ProjectHistories = project.ProjectHistories,
                    LastProjectStatus = lastProjectStatus,
                    Progress = 0
                };
            }
            else
            {
                projectVM = new DetailsProjectViewModel
                {
                    ProjectId = project.ProjectId,
                    ProjectTitle = project.ProjectTitle,
                    PlanStartDate = project.PlanStartDate,
                    PlanOverDate = project.PlanOverDate,
                    Works = project.Works,
                    ProjectHistories = project.ProjectHistories,
                    LastProjectStatus = lastProjectStatus,
                    Progress = (completedWorks * 100) / totalWorksCount
                };
            }


            return View(projectVM);
        }

        public async Task<ActionResult> ChangeStatus(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                var lastProjectStatus = await _projectHistoryRepository.GetLastProjectStatus(id);
                var projectHistory = new ProjectHistory { };
                if (lastProjectStatus == ProjectStatus.Idea) {
                    projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = ProjectStatus.Launch,
                        CreatedAt = DateTime.Now
                    };
                }
                else if (lastProjectStatus == ProjectStatus.Launch)
                {
                    projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = ProjectStatus.Planning,
                        CreatedAt = DateTime.Now
                    };
                } else if (lastProjectStatus == ProjectStatus.Planning)
                {
                    projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = ProjectStatus.Execution,
                        CreatedAt = DateTime.Now
                    };
                } else if (lastProjectStatus == ProjectStatus.Execution)
                {
                    projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = ProjectStatus.Completed,
                        CreatedAt = DateTime.Now
                    };
                }
                _projectHistoryRepository.Add(projectHistory);
            }
            return RedirectToAction("Details", new { id });
        }

        public async Task<ActionResult> Freeze(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                var lastProjectStatus = await _projectHistoryRepository.GetLastProjectStatus(id);
                var projectHistory = new ProjectHistory { };
                if (lastProjectStatus == ProjectStatus.Frozen)
                {
                    projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = await _projectHistoryRepository.GetPreviousProjectStatus(id),
                        CreatedAt = DateTime.Now
                    };
                }
                else
                {
                    projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = ProjectStatus.Frozen,
                        CreatedAt = DateTime.Now
                    };
                }
                    _projectHistoryRepository.Add(projectHistory);
            }
            return RedirectToAction("Details", new { id });
        }

        public async Task<ActionResult> Cancel(int id)
        {
            var project = await _projectRepository.GetByIdPlusDataAsync(id);
            if (project != null)
            {
                if (project.Works.Count == 0)
                {
                    var projectHistory = await _projectHistoryRepository.GetAll();
                    var projectHistoryToDelete = projectHistory.Where(ph => ph.ProjectId == id);
                    foreach (var ph in projectHistoryToDelete)
                    {
                        _projectHistoryRepository.Delete(ph);
                    }
                    _projectRepository.Delete(project);

                    return RedirectToAction("Index");

                }
                else
                {
                    var projectHistory = new ProjectHistory
                    {
                        ProjectId = id,
                        ProjectStatus = ProjectStatus.Cancelled,
                        CreatedAt = DateTime.Now
                    };
                    _projectHistoryRepository.Add(projectHistory);
                }
            }
            return RedirectToAction("Details", new { id });
        }

        [Authorize(Roles = "admin, project_manager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var projectVM = new ProjectViewModel{};
            return View(projectVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel projectVM)
        {
            if (projectVM.PlanStartDate > projectVM.PlanOverDate)
            {
                ModelState.AddModelError("PlanStartDate", "Дата начала не может быть позже даты окончания");
                return View(projectVM);
            }
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    ProjectTitle = projectVM.ProjectTitle,
                    PlanStartDate = projectVM.PlanStartDate,
                    PlanOverDate = projectVM.PlanOverDate
                };
                _projectRepository.Add(project);

                var projectHistory = new ProjectHistory
                {
                    ProjectId = project.ProjectId,
                    ProjectStatus = ProjectStatus.Idea,
                    CreatedAt = DateTime.Now
                };
                _projectHistoryRepository.Add(projectHistory);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }
            return View(projectVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return View("Error");
            }
            var projectVM = new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                ProjectTitle = project.ProjectTitle,
                PlanStartDate = project.PlanStartDate,
                PlanOverDate = project.PlanOverDate
            };
            return View(projectVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProjectViewModel projectVM)
        {
            if (projectVM.PlanStartDate > projectVM.PlanOverDate)
            {
                ModelState.AddModelError("PlanStartDate", "Дата начала не может быть позже даты окончания");
                return View(projectVM);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Edit", projectVM);
            }
            var project = new Project
            {
                ProjectId = id,
                ProjectTitle = projectVM.ProjectTitle,
                PlanStartDate = projectVM.PlanStartDate,
                PlanOverDate = projectVM.PlanOverDate
            };

            _projectRepository.Update(project);
            return RedirectToAction("Index");
        }
    }
}
