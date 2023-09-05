using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DesignBureauWebApplication.Controllers
{
    public class ConsumptionController : Controller
    {
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAssignmentHistoryRepository _assignmentHistoryRepository;

        public ConsumptionController (IConsumptionRepository consumptionRepository,
            IMaterialRepository materialRepository, IAssignmentRepository assignmentRepository,
            IAssignmentHistoryRepository assignmentHistoryRepository)
        {
            _consumptionRepository = consumptionRepository;
            _materialRepository = materialRepository;
            _assignmentRepository = assignmentRepository;
            _assignmentHistoryRepository = assignmentHistoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int assignmentId)
        {
            var materials = await _materialRepository.GetAllPlusData();
            var materialSelection = new Dictionary<int, bool>();
            var quantities = new Dictionary<int, int>();

            foreach (var material in materials)
            {
                materialSelection[material.MaterialId] = false; // по умолчанию не выбран
                quantities[material.MaterialId] = 0; // по умолчанию количество 0
            }

            var consumptionVM = new ConsumptionViewModel
            {
                AssignmentId = assignmentId,
                Materials = materials,
                MaterialSelection = materialSelection,
                Quantities = quantities
            };

            return View(consumptionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConsumptionViewModel consumptionVM)
        {
            if (!ModelState.IsValid)
            {
                var assignment = await _assignmentRepository.GetByIdPlusDataAsync(consumptionVM.AssignmentId);
                var assignmentStatus = await _assignmentHistoryRepository.GetLastAssignmentStatus(assignment.AssignmentId);
                if (assignment.Consumptions.Count == 0 && assignmentStatus == AssignmentStatus.Preparation)
                {
                    var assignmentHisotry = new AssignmentHistory
                    {
                        AssignmentId = assignment.AssignmentId,
                        AssignmentStatus = AssignmentStatus.InProgress,
                        CreatedAt = DateTime.Now
                    };
                    _assignmentHistoryRepository.Add(assignmentHisotry);
                }
                foreach (var materialSelection in consumptionVM.MaterialSelection)
                {
                    if (materialSelection.Value)
                    {
                        var materialId = materialSelection.Key;
                        var quantity = consumptionVM.Quantities[materialId];
                        var material = await _materialRepository.GetByIdPlusDataAsync(materialId);
                        material.Quantity -= quantity;
                        _materialRepository.Update(material);

                        var consumption = new Consumption
                        {
                            MaterialId = materialId,
                            Material = material,
                            Quantity = quantity,
                            AssignmentId = consumptionVM.AssignmentId,
                            CreatedAt = DateTime.Now
                        };
                        _consumptionRepository.Add(consumption);

                        return RedirectToAction("Details", "Assignment", new { id = consumption.AssignmentId });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
                return View(consumptionVM);
            }
            return View(consumptionVM);
        }
    }
}
