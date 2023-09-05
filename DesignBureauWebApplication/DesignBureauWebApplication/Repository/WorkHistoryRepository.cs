using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class WorkHistoryRepository : IWorkHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(WorkHistory WorkHistory)
        {
            _context.Add(WorkHistory);
            return Save();
        }

        public bool Update(WorkHistory WorkHistory)
        {
            _context.Update(WorkHistory);
            return Save();
        }

        public bool Delete(WorkHistory WorkHistory)
        {
            _context.Remove(WorkHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<WorkHistory>> GetAll()
        {
            return await _context.WorkHistory.Include(w => w.Work).ToListAsync();
        }

        public async Task<WorkHistory> GetByIdAsync(int id)
        {
            return await _context.WorkHistory.Include(w => w.Work).FirstOrDefaultAsync(wh => wh.WorkHistoryId == id);
        }

        public async Task<WorkStatus> GetLastWorkStatus(int id)
        {
            return await _context.Work.Where(w => w.WorkId == id)
                .SelectMany(w => w.WorkHistories)
                .OrderByDescending(wh => wh.CreatedAt)
                .Select(wh => wh.WorkStatus)
                .FirstOrDefaultAsync();
        }
    }
}
