using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var suppliers = _context.Supplier.ToList();
            return View(suppliers);
        }

        public IActionResult Details(int id)
        {
            Supplier supplier = _context.Supplier.FirstOrDefault(s => s.SupplierId == id);
            return View();
        }
    }
}
