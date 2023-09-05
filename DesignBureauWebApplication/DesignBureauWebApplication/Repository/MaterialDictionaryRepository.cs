using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class MaterialDictionaryRepository : IMaterialDictionaryRepository
    {
        private readonly ApplicationDbContext _context;
        public MaterialDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(MaterialDictionary MaterialDictionary)
        {
            _context.Add(MaterialDictionary);
            return Save();
        }

        public bool Delete(MaterialDictionary MaterialDictionary)
        {
            _context.Remove(MaterialDictionary);
            return Save();
        }

        public async Task<IEnumerable<MaterialDictionary>> GetAll()
        {
            return await _context.MaterialDictionary
                .ToListAsync();
        }

        public async Task<MaterialDictionary?> GetByIdAsync(int id)
        {
            return await _context.MaterialDictionary
                .FirstOrDefaultAsync(md => md.MaterialDictionaryId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(MaterialDictionary MaterialDictionary)
        {
            _context.Update(MaterialDictionary);
            return Save();
        }
    }
}
