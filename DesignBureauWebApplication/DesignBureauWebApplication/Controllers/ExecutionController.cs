using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;

namespace DesignBureauWebApplication.Controllers
{
    public class ExecutionController : Controller
    {
        private readonly IExecutionRepository _executionRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAssignmentHistoryRepository _assignmentHistoryRepository;

        public ExecutionController(IExecutionRepository executionRepository,
            IAssignmentRepository assignmentRepository, IEmployeeRepository employeeRepository, IAssignmentHistoryRepository assignmentHistoryRepository)
        {
            _executionRepository = executionRepository;
            _assignmentRepository = assignmentRepository;
            _employeeRepository = employeeRepository;
            _assignmentHistoryRepository = assignmentHistoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var execution = await _executionRepository.GetByIdAsync(id);
            if (execution == null)
            {
                return NotFound();
            }

            _executionRepository.Delete(execution);

            return RedirectToAction("Details", "Assignment", new { id = execution.AssignmentId });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int assignmentId)
        {
            var assignment = await _assignmentRepository.GetByIdPlusDataAsync(assignmentId);
            var employees = await _employeeRepository.GetAll();
            var employeeList = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.Position.JobTitle + ": " + e.ELastName + " " + e.EFirstName + " " + e.EPatronymic
            }).OrderBy(e => e.Text).ToList();

            var executionVM = new ExecutionViewModel
            {
                AssignmentId = assignmentId,
                Assignment = assignment,
                EmployeeList = new SelectList(employeeList, "Value", "Text")
            };

            return View(executionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExecutionViewModel executionVM)
        {
            //if (_executionRepository.FindByAssignmentIdAndEmployeeIdAsync(executionVM.AssignmentId, executionVM.EmployeeId) != null)
            //{
            //    ModelState.AddModelError("EmployeeId", "Этот сотрудник уже назначен на данную задачу");
            //    //return View(executionVM);
            //    return RedirectToAction("Details", "Assignment", new { id = executionVM.AssignmentId });
            //}
            if (ModelState.IsValid)
            {
                var assignment = await _assignmentRepository.GetByIdPlusDataAsync(executionVM.AssignmentId);
                var assignmentStatus = await _assignmentHistoryRepository.GetLastAssignmentStatus(assignment.AssignmentId);
                if (assignment.Executions.Count == 0 && assignmentStatus == AssignmentStatus.Created)
                {
                    var assignmentHisotry = new AssignmentHistory
                    {
                        AssignmentId = assignment.AssignmentId,
                        AssignmentStatus = AssignmentStatus.Preparation,
                        CreatedAt = DateTime.Now
                    };
                    _assignmentHistoryRepository.Add(assignmentHisotry);
                }
                    var execution = new Execution
                {
                    AssignmentId= executionVM.AssignmentId,
                    EmployeeId = executionVM.EmployeeId
                };
                _executionRepository.Add(execution);

                return RedirectToAction("Details", "Assignment", new { id = execution.AssignmentId });
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
                return View(executionVM);
            }
            return View(executionVM);
        }
    }
}
