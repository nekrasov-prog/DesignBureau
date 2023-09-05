using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class WorkDictionaryRepository : IWorkDictionaryRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(WorkDictionary workDictionary)
        {
            _context.Add(workDictionary);
            return Save();
        }

        public bool Update(WorkDictionary workDictionary)
        {
            _context.Update(workDictionary);
            return Save();
        }

        public bool Delete(WorkDictionary workDictionary)
        {
            _context.Remove(workDictionary);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<WorkDictionary>> GetAll()
        {
            return await _context.WorkDictionary.ToListAsync();
        }

        public async Task<WorkDictionary> GetByIdAsync(int id)
        {
            return await _context.WorkDictionary.FirstOrDefaultAsync(wd => wd.WorkDictionaryId == id);
        }
    }
}
