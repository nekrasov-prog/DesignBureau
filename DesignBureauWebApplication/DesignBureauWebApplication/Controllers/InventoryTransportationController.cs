using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.ViewModels;
using DesignBureauWebApplication.Data.Enum;

namespace DesignBureauWebApplication.Controllers
{
    public class InventoryTransportationController : Controller
    {
        private readonly IInventoryTransportationRepository _inventoryTransportationRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ITransportationRepository _transportationRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IEquipmentHistoryRepository _equipmentHistoryRepository;

        public InventoryTransportationController(IInventoryTransportationRepository inventoryTransportationRepository,
            IInventoryRepository inventoryRepository, ITransportationRepository transportationRepository,
            IEquipmentRepository equipmentRepository, IMaterialRepository materialRepository,
            IEquipmentHistoryRepository equipmentHistoryRepository)
        {
            _inventoryTransportationRepository = inventoryTransportationRepository;
            _inventoryRepository = inventoryRepository;
            _transportationRepository = transportationRepository;
            _equipmentRepository = equipmentRepository;
            _materialRepository = materialRepository;
            _equipmentHistoryRepository = equipmentHistoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int transportationId)
        {
            var transportation = await _transportationRepository.GetByIdAsync(transportationId);
            if (transportation == null)
            {
                return NotFound();
            }

            var allInventories = await _inventoryRepository.GetAll();
            if (allInventories == null)
            {
                return NotFound();
            }

            var inventories = allInventories
                                .Where(i => i.LocationId == transportation.OriginId && (!i.InventoryTransportations.Any() ||
                                            i.InventoryTransportations.Any() ||
                                            i.InventoryTransportations.Any(it => it.Transportation.TransportationHistories
                                                    .OrderByDescending(th => th.CreatedAt)
                                                    .FirstOrDefault()?.TransportationStatus != TransportationStatus.Delivered &&
                                                 it.Transportation.TransportationHistories
                                                    .OrderByDescending(th => th.CreatedAt)
                                                    .FirstOrDefault()?.TransportationStatus != TransportationStatus.Cancelled)))
                                .ToList();

            var inventorySelection = new Dictionary<int, bool>();
            var quantities = new Dictionary<int, int>();

            foreach (var inventory in inventories)
            {
                inventorySelection[inventory.InventoryId] = false;
                if (inventory.EquipmentId != null)
                {
                    quantities[inventory.InventoryId] = 1;
                }
                else
                {
                    quantities[inventory.InventoryId] = 0;
                }
            }

            var inventoryTransportationVM = new InventoryTransportationViewModel
            {
                TransportationId = transportationId,
                Inventories = inventories,
                InventorySelection = inventorySelection,
                Quantities = quantities
            };

            return View(inventoryTransportationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InventoryTransportationViewModel inventoryTransportationVM)
        {
            if (ModelState.IsValid)
            {
                foreach(var inventorySelection in inventoryTransportationVM.InventorySelection)
                {
                    if (inventorySelection.Value)
                    {
                        var inventoryId = inventorySelection.Key;
                        var quantity = inventoryTransportationVM.Quantities[inventoryId];
                        var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
                        if (inventory.MaterialId != null)
                        {
                            inventory.Material.Quantity -= quantity;
                            _materialRepository.Update(inventory.Material);
                        }

                        var inventoryTransportation = new InventoryTransportation
                        {
                            InventoryId = inventoryId,
                            TransportationId = inventoryTransportationVM.TransportationId,
                            Quantity = quantity
                        };
                        _inventoryTransportationRepository.Add(inventoryTransportation);

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
                return View(inventoryTransportationVM);
            }

            return RedirectToAction("Details", "Transportation", new { id = inventoryTransportationVM.TransportationId });
        }
    }
}
