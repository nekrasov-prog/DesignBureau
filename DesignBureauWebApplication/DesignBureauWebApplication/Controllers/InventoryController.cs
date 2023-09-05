using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Repository;

namespace DesignBureauWebApplication.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Inventory> inventories = await _inventoryRepository.GetAll();
            return View(inventories);
        }

        public async Task<IActionResult> Details(int id)
        {
            Inventory inventory = await _inventoryRepository.GetByIdAsync(id);
            return View(inventory);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return View(inventory);
            }
            _inventoryRepository.Add(inventory);
            return RedirectToAction("Index");
        }
    }
}
