using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class AssignmentDictionaryRepository : IAssignmentDictionaryRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignmentDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(AssignmentDictionary AssignmentDictionary)
        {
            _context.Add(AssignmentDictionary);
            return Save();
        }

        public bool Update(AssignmentDictionary AssignmentDictionary)
        {
            _context.Update(AssignmentDictionary);
            return Save();
        }

        public bool Delete(AssignmentDictionary AssignmentDictionary)
        {
            _context.Remove(AssignmentDictionary);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<AssignmentDictionary>> GetAll()
        {
            return await _context.AssignmentDictionary.ToListAsync();
        }

        public async Task<AssignmentDictionary> GetByIdAsync(int id)
        {
            return await _context.AssignmentDictionary.FirstOrDefaultAsync(ad => ad.AssignmentDictionaryId == id);
        }
    }
}
