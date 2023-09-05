using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var locations = _context.Location.ToList();
            return View(locations);
        }

        public IActionResult Details(int id)
        {
            var location = _context.Location.FirstOrDefault(l => l.LocationId == id);
            return View(location);
        }
    }
}
