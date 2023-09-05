using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Controllers
{
    public class WorkDictionaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkDictionaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var workDictionaries = _context.WorkDictionary.ToList();
            return View(workDictionaries);
        }

        public IActionResult Details(int id)
        {
            WorkDictionary workDictionary = _context.WorkDictionary.FirstOrDefault(wd => wd.WorkDictionaryId == id);
            return View(workDictionary);
        }
    }
}
