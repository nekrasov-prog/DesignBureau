//using DesignBureauWebApplication.Data;
//using DesignBureauWebApplication.Interfaces;
//using DesignBureauWebApplication.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DesignBureauWebApplication.Repository
//{
//    public class InteractionDictionaryRepository : IInteractionDictionaryRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public InteractionDictionaryRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public bool Add(InteractionDictionary interactionDictionary)
//        {
//            _context.Add(interactionDictionary);
//            return Save();
//        }

//        public bool Update(InteractionDictionary interactionDictionary)
//        {
//            _context.Update(interactionDictionary);
//            return Save();
//        }

//        public bool Delete(InteractionDictionary interactionDictionary)
//        {
//            _context.Remove(interactionDictionary);
//            return Save();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public async Task<IEnumerable<InteractionDictionary>> GetAll()
//        {
//            return await _context.InteractionDictionary.ToListAsync();
//        }

//        public async Task<InteractionDictionary> GetByIdAsync(int id)
//        {
//            return await _context.InteractionDictionary.FirstOrDefaultAsync(i_d => i_d.InteractionDictionaryId == id);
//        }
//    }
//}
