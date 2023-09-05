using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Assignment assignment)
        {
            _context.Add(assignment);
            return Save();
        }

        public bool Update(Assignment assignment)
        {
            _context.Update(assignment);
            return Save();
        }

        public bool Delete(Assignment assignment)
        {
            _context.Remove(assignment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Assignment>> GetAll()
        {
            return await _context.Assignment
                .Include(ad => ad.AssignmentDictionary)
                .Include(w => w.Work)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAllPlusData()
        {
            return await _context.Assignment
                .Include(ad => ad.AssignmentDictionary)
                .Include(a => a.Work)
                    .ThenInclude(w => w.WorkDictionary)
                .Include(a => a.Work)
                    .ThenInclude(w => w.Project)
                .ToListAsync();
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            return await _context.Assignment
                .Include(ad => ad.AssignmentDictionary)
                .Include(w => w.Work)
                    .ThenInclude(wd => wd.WorkDictionary)
                .FirstOrDefaultAsync(a => a.AssignmentId == id);
        }

        public async Task<Assignment> GetByIdPlusDataAsync(int id)
        {
            return await _context.Assignment
                .Include(ad => ad.AssignmentDictionary)
                .Include(w => w.Work)
                    .ThenInclude(wd => wd.WorkDictionary)
                .Include(w => w.Work)
                    .ThenInclude(p => p.Project)
                .Include(a => a.AssignmentHistories)
                .Include(a => a.Consumptions)
                    .ThenInclude(c => c.Material)
                        .ThenInclude(m => m.MaterialDictionary)
                .Include(a => a.Executions)
                    .ThenInclude(e => e.Employee)
                        .ThenInclude(e => e.Position)
                .FirstOrDefaultAsync(a => a.AssignmentId == id);
        }
    }
}
