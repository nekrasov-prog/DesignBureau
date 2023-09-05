using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.Repository;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IEquipmentHistoryRepository _equipmentHistoryRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository,
            IInventoryRepository inventoryRepository, IOrderRepository orderRepository,
            IEquipmentRepository equipmentRepository, IMaterialRepository materialRepository,
            IEquipmentHistoryRepository equipmentHistoryRepository)
        {
            _orderItemRepository = orderItemRepository;
            _inventoryRepository = inventoryRepository;
            _orderRepository = orderRepository;
            _equipmentRepository = equipmentRepository;
            _materialRepository = materialRepository;
            _equipmentHistoryRepository = equipmentHistoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int orderId)
        {
            var equipments = await _orderItemRepository.GetAllEquipmentDictionaries();
            equipments.Insert(0, new EquipmentDictionary { EquipmentDictionaryId = 0, EquipmentName = "Выберите оборудование" });
            var materials = await _orderItemRepository.GetAllMaterialDictionaries();
            materials.Insert(0, new MaterialDictionary { MaterialDictionaryId = 0, MaterialName = "Выберите материал" });
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var orderItemVM = new OrderItemViewModel
            {
                OrderId = orderId,
                Order = order,
                EquipmentDictionaryList = new SelectList(equipments, "EquipmentDictionaryId", "EquipmentName"),
                MaterialDictionaryList = new SelectList(materials, "MaterialDictionaryId", "MaterialName")
            };

            return View(orderItemVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItemViewModel orderItemVM)
        {
            if (ModelState.IsValid)
            {
                int selectedEquipmentDictionaryId = orderItemVM.SelectedEquipmentDictionaryId;
                int selectedMaterialDictionaryId = orderItemVM.SelectedMaterialDictionaryId;

                if (selectedEquipmentDictionaryId != 0 && selectedMaterialDictionaryId == 0)
                {
                    for (int i = 0; i < orderItemVM.Quantity; i++)
                    {
                        var equipment = new Equipment
                        {
                            EquipmentDictionaryId = selectedEquipmentDictionaryId
                        };
                        _equipmentRepository.Add(equipment);

                        var inventory = new Inventory
                        {
                            EquipmentId = equipment.EquipmentId,
                            MaterialId = null,
                            LocationId = null
                        };
                        _inventoryRepository.Add(inventory);

                        var equipmentHistory = new EquipmentHistory
                        {
                            EquipmentId = equipment.EquipmentId,
                            EquipmentStatus = EquipmentStatus.New,
                            StatusStart = DateTime.Now,
                            StatusEnd = null
                        };
                        _equipmentHistoryRepository.Add(equipmentHistory);

                        var orderItem = new OrderItem
                        {
                            OrderId = orderItemVM.OrderId,
                            InventoryId = inventory.InventoryId,
                            Cost = orderItemVM.Cost / orderItemVM.Quantity
                        };
                        _orderItemRepository.Add(orderItem);
                    }
                }
                else if (selectedEquipmentDictionaryId == 0 && selectedMaterialDictionaryId != 0)
                {
                    var material = new Material
                    {
                        MaterialDictionaryId = selectedMaterialDictionaryId,
                        Quantity = orderItemVM.Quantity
                    };
                    _materialRepository.Add(material);

                    var inventory = new Inventory
                    {
                        EquipmentId = null,
                        MaterialId = material.MaterialId,
                        LocationId = null
                    };
                    _inventoryRepository.Add(inventory);

                    var orderItem = new OrderItem
                    {
                        OrderId = orderItemVM.OrderId,
                        InventoryId = inventory.InventoryId,
                        Cost = orderItemVM.Cost
                    };
                    _orderItemRepository.Add(orderItem);
                }
                return RedirectToAction("Details", "Order", new { id = orderItemVM.OrderId });
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }

            return View(orderItemVM);
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var orderItem = await _orderItemRepository.GetByIdPlusDataAsync(id);
        //    var selectedEquipmentId = orderItem.Inventory.Equipment?.EquipmentDictionaryId ?? 0;
        //    var equipments = await _orderItemRepository.GetAllEquipmentDictionaries();
        //    var equipmentDictionaryList = new SelectList(equipments, "EquipmentDictionaryId", "EquipmentName", selectedEquipmentId);
        //    var selectedMaterialId = orderItem.Inventory.Material?.MaterialDictionaryId ?? 0;
        //    var materials = await _orderItemRepository.GetAllMaterialDictionaries();
        //    var materialDictionaryList = new SelectList(materials, "MaterialDictionaryId", "MaterialName", selectedMaterialId);

        //    if (orderItem == null)
        //    {
        //        return View("Error");
        //    }

        //    var orderItemVM = new OrderItemViewModel
        //    {
        //        OrderItemId = orderItem.OrderItemId,
        //        OrderId = orderItem.OrderId,
        //        EquipmentDictionaryList = new SelectList(equipments, "EquipmentDictionaryId", "EquipmentName"),
        //        MaterialDictionaryList = new SelectList(materials, "MaterialDictionaryId", "MaterialName"),
        //        Cost = orderItem.Cost
        //    };



        //    return View(orderItemVM);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, OrderItemViewModel orderItemVM)
        //{
        //    return RedirectToAction("Index");
        //}
    }
}
