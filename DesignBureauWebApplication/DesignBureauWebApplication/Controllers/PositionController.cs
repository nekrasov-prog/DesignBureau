using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Controllers
{
    public class PositionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var positions = _context.Position.ToList();
            return View(positions);
        }

        public IActionResult Details(int id)
        {
            Position position = _context.Position
                .FirstOrDefault(p => p.PositionId == id);
            return View(position);
        }
    }
}
