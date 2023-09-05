//using DesignBureauWebApplication.Data;
//using DesignBureauWebApplication.Interfaces;
//using DesignBureauWebApplication.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DesignBureauWebApplication.Repository
//{
//    public class InteractionRepository : IInteractionRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public InteractionRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public bool Add(Interaction interaction)
//        {
//            _context.Add(interaction);
//            return Save();
//        }

//        public bool Update(Interaction interaction)
//        {
//            _context.Update(interaction);
//            return Save();
//        }

//        public bool Delete(Interaction interaction)
//        {
//            _context.Remove(interaction);
//            return Save();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public async Task<IEnumerable<Interaction>> GetAll()
//        {
//            return await _context.Interaction
//                .Include(i_d => i_d.InteractionDictionary)
//                .Include(w => w.Work)
//                .Include(inv => inv.Inventory)
//                .Include(e => e.Employee)
//                .ToListAsync();
//        }

//        public async Task<Interaction> GetByIdAsync(int id)
//        {
//            return await _context.Interaction
//                .Include(i_d => i_d.InteractionDictionary)
//                .Include(w => w.Work)
//                .Include(inv => inv.Inventory)
//                .Include(e => e.Employee)
//                .FirstOrDefaultAsync(i => i.InteractionId == id);
//        }
//    }
//}
