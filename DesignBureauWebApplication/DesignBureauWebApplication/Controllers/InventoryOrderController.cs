//using Microsoft.AspNetCore.Mvc;
//using DesignBureauWebApplication.Data;
//using Microsoft.EntityFrameworkCore;
//using DesignBureauWebApplication.Models;

//namespace DesignBureauWebApplication.Controllers
//{
//    public class InventoryOrderController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public InventoryOrderController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var inventoryOrders = _context.InventoryOrder
//                .Include(i => i.Inventory)
//                .Include(o => o.Order)
//                .ToList();
//            return View(inventoryOrders);
//        }

//        public IActionResult Details(int id)
//        {
//            InventoryOrder inventoryOrder = _context.InventoryOrder
//                .Include(i => i.Inventory)
//                .Include(o => o.Order)
//                .FirstOrDefault(io => io.InventoryOrderId == id);
//            return View(inventoryOrder);
//        }
//    }
//}
