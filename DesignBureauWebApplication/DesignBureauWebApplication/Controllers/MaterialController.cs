using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DesignBureauWebApplication.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaterialDictionaryRepository _materialDictionaryRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public MaterialController(IMaterialRepository materialRepository, IMaterialDictionaryRepository materialDictionaryRepository,
            IInventoryRepository inventoryRepository, ILocationRepository locationRepository, IConsumptionRepository consumptionRepository,
            IOrderHistoryRepository orderHistoryRepository)
        {
            _materialRepository = materialRepository;
            _materialDictionaryRepository = materialDictionaryRepository;
            _inventoryRepository = inventoryRepository;
            _locationRepository = locationRepository;
            _consumptionRepository = consumptionRepository;
            _orderHistoryRepository = orderHistoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var materialList = await _materialRepository.GetAllPlusData();
            var materialsViewModel = new List<DetailsMaterialViewModel>();

            foreach (var material in materialList)
            {
                var whereIs = "";
                int? orderId = null;
                if (await _materialRepository.IsOrderedAsync(material.MaterialId))
                {
                    var order = await _materialRepository.GetOrderByMaterial(material.MaterialId);
                    if (order != null)
                    {
                        orderId = order.OrderId;
                        var orderStatus = await _orderHistoryRepository.GetLastOrderStatus(order.OrderId);
                        var memberInfo = typeof(OrderStatus).GetMember(orderStatus.ToString());
                        var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                        var displayOrderStatus = displayAttribute?.Name ?? orderStatus.ToString();
                        whereIs = "Заказ " + order.ContractNumber + ": " + displayOrderStatus;
                    }
                }
                materialsViewModel.Add(new DetailsMaterialViewModel
                {
                    MaterialId = material.MaterialId,
                    MaterialDictionaryId = material.MaterialDictionaryId,
                    MaterialDictionary = material.MaterialDictionary,
                    Inventory = material.Inventory,
                    Consumptions = material.Consumptions.OrderByDescending(eh => eh.CreatedAt).ToList(),
                    Quantity = material.Quantity,
                    WhereIs = whereIs,
                    OrderId = orderId
                });
            }

            return View(materialsViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Material material = await _materialRepository.GetByIdPlusDataAsync(id);
            if (material == null)
            {
                return View("Error");
            }

            var whereIs = "";
            int? orderId = null;
            if (await _materialRepository.IsOrderedAsync(id))
            {
                var order = await _materialRepository.GetOrderByMaterial(id);
                if (order != null)
                {
                    orderId = order.OrderId;
                    var orderStatus = await _orderHistoryRepository.GetLastOrderStatus(order.OrderId);
                    var memberInfo = typeof(OrderStatus).GetMember(orderStatus.ToString());
                    var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    var displayOrderStatus = displayAttribute?.Name ?? orderStatus.ToString();
                    whereIs = "Заказ " + order.ContractNumber + ": " + displayOrderStatus;
                }
            }

            var materialVM = new DetailsMaterialViewModel
            {
                MaterialId = material.MaterialId,
                MaterialDictionaryId = material.MaterialDictionaryId,
                MaterialDictionary = material.MaterialDictionary,
                Quantity = material.Quantity,
                Inventory = material.Inventory,
                Consumptions = material.Consumptions,
                WhereIs = whereIs,
                OrderId = orderId
            };

            return View(materialVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var materialDictionaries = await _materialRepository.GetAllMaterialDictionaries();
            var locations = await _locationRepository.GetAll();
            var locationList = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.City + ", " + l.Street + ", " + l.HouseNumber
            }).ToList();

            var materialVM = new MaterialViewModel
            {
                MaterialDictionaryList = new SelectList(materialDictionaries, "MaterialDictionaryId", "MaterialName"),
                LocationList = new SelectList(locationList, "Value", "Text")
            };

            return View(materialVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaterialViewModel materialVM)
        {
            if (ModelState.IsValid)
            {
                var materialDictionary = await _materialRepository.GetMaterialDictionaryById(materialVM.MaterialDictionaryId);
                var existingInventory = await _inventoryRepository.GetByMaterialNameAndLocation(materialDictionary.MaterialName, materialVM.LocationId);
                if (existingInventory != null)
                {
                    var existingMaterial = await _materialRepository.GetByIdAsync(existingInventory.MaterialId.GetValueOrDefault());
                    if (existingMaterial != null)
                    {
                        existingMaterial.Quantity += materialVM.Quantity;
                        _materialRepository.Update(existingMaterial);
                    }
                }
                else
                {
                    var material = new Material
                    {
                        MaterialDictionaryId = materialVM.MaterialDictionaryId,
                        Quantity = materialVM.Quantity
                    };
                    _materialRepository.Add(material);

                    var inventory = new Inventory
                    {
                        MaterialId = material.MaterialId,
                        EquipmentId = null,
                        LocationId = materialVM.LocationId
                    };
                    _inventoryRepository.Add(inventory);
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }

            materialVM.MaterialDictionaryList = new SelectList(await _materialRepository.GetAllMaterialDictionaries(), "MaterialDictionaryId", "MaterialName");
            materialVM.LocationList = new SelectList(await _locationRepository.GetAll(), "Value", "Text");
            return View(materialVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _materialRepository.GetByIdPlusDataAsync(id);
            var locations = await _locationRepository.GetAll();
            var locationList = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.City + ", " + l.Street + ", " + l.HouseNumber,
                Selected = (l.LocationId == material.Inventory.LocationId)
            }).ToList();

            if (material == null)
            {
                return View("Error");
            }

            var materialVM = new MaterialViewModel
            {
                MaterialId = id,
                MaterialDictionaryId = material.MaterialDictionaryId,
                MaterialDictionaryList = new SelectList(await _materialRepository.GetAllMaterialDictionaries(), "MaterialDictionaryId", "MaterialName"),
                LocationId = material.Inventory.LocationId,
                LocationList = new SelectList(locationList, "Value", "Text"),
                Quantity = material.Quantity
            };
            return View(materialVM);
        }

        /*[HttpPost]
        public async Task<IActionResult> Edit(int id, MaterialViewModel materialVM)
        {
            if (ModelState.IsValid)
            {
                var material = new Material
                {
                    MaterialId = id,
                    MaterialDictionaryId = materialVM.MaterialDictionaryId,
                    Quantity = materialVM.Quantity
                };
                _materialRepository.Update(material);

                var inventory = await _inventoryRepository.GetInventoryByMaterialId(id);
                if (inventory != null)
                {
                    inventory.LocationId = materialVM.LocationId;
                    _inventoryRepository.Update(inventory);
                }
                else
                {
                    ModelState.AddModelError("", "Не удалось найти запись инвентаря для данного оборудования");
                    return View("Error", materialVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Error", materialVM);
            }
            return RedirectToAction("Index");
        }*/
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MaterialViewModel materialVM)
        {
            if (ModelState.IsValid)
            {
                var material = await _materialRepository.GetByIdAsync(id);
                if (material != null)
                {
                    // Update the material with new values
                    material.MaterialDictionaryId = materialVM.MaterialDictionaryId;
                    material.Quantity = materialVM.Quantity;
                    _materialRepository.Update(material);

                    var inventory = await _inventoryRepository.GetInventoryByMaterialId(id);
                    if (inventory != null)
                    {
                        // Check if the material name and location are equal to another record
                        var duplicateMaterial = await _materialRepository.GetMaterialByDictionaryAndLocation(material.MaterialDictionaryId, materialVM.LocationId);
                        if (duplicateMaterial != null && duplicateMaterial.MaterialId != material.MaterialId)
                        {
                            // Delete the current material and add the quantity to the duplicate record
                            duplicateMaterial.Quantity += material.Quantity;
                            _materialRepository.Delete(material);
                            _materialRepository.Update(duplicateMaterial);

                            // Delete the inventory record for the current material
                            _inventoryRepository.Delete(inventory);
                        }
                        else
                        {
                            // Update the inventory record with the new location
                            inventory.LocationId = materialVM.LocationId;
                            _inventoryRepository.Update(inventory);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не удалось найти запись инвентаря для данного оборудования");
                        return View("Error", materialVM);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Не удалось найти материал с указанным идентификатором");
                    return View("Error", materialVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Error", materialVM);
            }
            return RedirectToAction("Index");
        }

    }
}
