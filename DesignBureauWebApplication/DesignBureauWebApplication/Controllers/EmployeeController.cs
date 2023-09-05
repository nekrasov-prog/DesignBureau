using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employee.Include(p => p.Position).ToList();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            Employee employee = _context.Employee.Include(p => p.Position).FirstOrDefault(e => e.EmployeeId == id);
            return View(employee);
        }
    }
}
