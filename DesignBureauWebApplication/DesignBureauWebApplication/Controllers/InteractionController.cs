//using Microsoft.AspNetCore.Mvc;
//using DesignBureauWebApplication.Data;
//using Microsoft.EntityFrameworkCore;
//using DesignBureauWebApplication.Models;

//namespace DesignBureauWebApplication.Controllers
//{
//    public class InteractionController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public InteractionController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var interactions = _context.Interaction
//                .Include(i_d => i_d.InteractionDictionary)
//                .Include(w => w.Work)
//                .Include(wd => wd.Work.WorkDictionary)
//                .Include(inv => inv.Inventory)
//                .Include(e => e.Employee)
//                .ToList();
//            return View(interactions);
//        }

//        public IActionResult Details(int id)
//        {
//            Interaction interaction = _context.Interaction
//                .Include(i_d => i_d.InteractionDictionary)
//                .Include(w => w.Work)
//                .Include(wd => wd.Work.WorkDictionary)
//                .Include(inv => inv.Inventory)
//                .Include(e => e.Employee)
//                .FirstOrDefault(i => i.InteractionId == id);
//            return View(interaction);
//        }
//    }
//}
