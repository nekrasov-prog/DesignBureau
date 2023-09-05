using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentDictionaryRepository _equipmentDictionaryRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IEquipmentHistoryRepository _equipmentHistoryRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository, IEquipmentDictionaryRepository equipmentDictionaryRepository,
            IInventoryRepository inventoryRepository, ILocationRepository locationRepository, IEquipmentHistoryRepository equipmentHistoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentDictionaryRepository = equipmentDictionaryRepository;
            _inventoryRepository = inventoryRepository;
            _locationRepository = locationRepository;
            _equipmentHistoryRepository = equipmentHistoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var equipmentList = await _equipmentRepository.GetAllPlusData();
            var equipmentsViewModel = new List<DetailsEquipmentViewModel>();
            foreach (var equipment in equipmentList)
            {
                equipmentsViewModel.Add(new DetailsEquipmentViewModel
                {
                    EquipmentId = equipment.EquipmentId,
                    EquipmentDictionaryId = equipment.EquipmentDictionaryId,
                    EquipmentDictionary = equipment.EquipmentDictionary,
                    ActualEquipmentStatus = await _equipmentHistoryRepository.GetActualEquipmentStatus(equipment.EquipmentId),
                    Inventory = equipment.Inventory,
                    EquipmentHistories = equipment.EquipmentHistories.OrderByDescending(eh => eh.StatusStart).ToList()
                });
            }

            return View(equipmentsViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Equipment equipment = await _equipmentRepository.GetByIdPlusDataAsync(id);
            if (equipment == null)
            {
                return View("Error");
            }

            var actualEquipmentStatus = await _equipmentHistoryRepository.GetActualEquipmentStatus(id);

            var equipmentVM = new DetailsEquipmentViewModel
            {
                EquipmentId = equipment.EquipmentId,
                EquipmentDictionaryId = equipment.EquipmentDictionaryId,
                EquipmentDictionary = equipment.EquipmentDictionary,
                ActualEquipmentStatus = actualEquipmentStatus,
                Inventory = equipment.Inventory,
                EquipmentHistories = equipment.EquipmentHistories
            };

            return View(equipmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var equipmentDictionaries = await _equipmentRepository.GetAllEquipmentDictionaries();
            /*var equipmentStatuses = Enum.GetValues(typeof(EquipmentStatus))
                                .Cast<EquipmentStatus>()
                                .Select(e => new SelectListItem
                                {
                                    Text = e.ToString(),
                                    Value = ((int)e).ToString()
                                }).ToList();*/
            var locations = await _locationRepository.GetAll();
            var locationList = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.City + ", " + l.Street + ", " + l.HouseNumber
            }).ToList();

            var equipmentVM = new EquipmentViewModel
            {
                EquipmentDictionaryList = new SelectList(equipmentDictionaries, "EquipmentDictionaryId", "EquipmentName"),
                //EquipmentStatus = EquipmentStatus.New,
                LocationList = new SelectList(locationList, "Value", "Text")
            };

            return View(equipmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EquipmentViewModel equipmentVM)
        {
            if (ModelState.IsValid)
            {
                var equipment = new Equipment
                {
                    EquipmentDictionaryId = equipmentVM.EquipmentDictionaryId,
                    //EquipmentDictionary = equipmentVM.EquipmentDictionary,
                };
                _equipmentRepository.Add(equipment);

                var inventory = new Inventory
                {
                    EquipmentId = equipment.EquipmentId,
                    MaterialId = null,
                    LocationId = equipmentVM.LocationId
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

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }

            equipmentVM.EquipmentDictionaryList = new SelectList(await _equipmentRepository.GetAllEquipmentDictionaries(), "EquipmentDictionaryId", "EquipmentName");
            equipmentVM.LocationList = new SelectList(await _locationRepository.GetAll(), "Value", "Text");
            return View(equipmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var equipment = await _equipmentRepository.GetByIdPlusDataAsync(id);
            var locations = await _locationRepository.GetAll();
            var locationList = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.City + ", " + l.Street + ", " + l.HouseNumber,
                Selected = (l.LocationId == equipment.Inventory.LocationId)
            }).ToList();

            if (equipment == null)
            {
                return View("Error");
            }

            var equipmentVM = new EquipmentViewModel
            {
                EquipmentId = id,
                EquipmentDictionaryId = equipment.EquipmentDictionaryId,
                EquipmentDictionaryList = new SelectList(await _equipmentRepository.GetAllEquipmentDictionaries(), "EquipmentDictionaryId", "EquipmentName"),
                LocationId = equipment.Inventory.LocationId,
                LocationList = new SelectList(locationList, "Value", "Text")
            };
            return View(equipmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EquipmentViewModel equipmentVM)
        {
            if (ModelState.IsValid)
            {
                var equipment = new Equipment
                {
                    EquipmentId = id,
                    EquipmentDictionaryId = equipmentVM.EquipmentDictionaryId,
                };
                _equipmentRepository.Update(equipment);

                var inventory = await _inventoryRepository.GetInventoryByEquipmentId(id);
                if (inventory != null)
                {
                    inventory.LocationId = equipmentVM.LocationId;
                    _inventoryRepository.Update(inventory);
                }
                else
                {
                    ModelState.AddModelError("", "Не удалось найти запись инвентаря для данного оборудования");
                    return View("Error", equipmentVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Error", equipmentVM);
            }
            return RedirectToAction("Index");
        }
    }
}
