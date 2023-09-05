//using Microsoft.AspNetCore.Mvc;
//using DesignBureauWebApplication.Data;
//using Microsoft.EntityFrameworkCore;
//using DesignBureauWebApplication.Models;

//namespace DesignBureauWebApplication.Controllers
//{
//    public class DepartmentController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public DepartmentController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var departments = _context.Department.Include(l => l.Location).ToList();
//            return View(departments);
//        }

//        public IActionResult Details(int id)
//        {
//            Department department = _context.Department.Include(l => l.Location).FirstOrDefault(d => d.DepartmentId == id);
//            return View(department);
//        }
//    }
//}
