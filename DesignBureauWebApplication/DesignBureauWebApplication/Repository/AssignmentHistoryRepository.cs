using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class AssignmentHistoryRepository : IAssignmentHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignmentHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(AssignmentHistory AssignmentHistory)
        {
            _context.Add(AssignmentHistory);
            return Save();
        }

        public bool Update(AssignmentHistory AssignmentHistory)
        {
            _context.Update(AssignmentHistory);
            return Save();
        }

        public bool Delete(AssignmentHistory AssignmentHistory)
        {
            _context.Remove(AssignmentHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<AssignmentHistory>> GetAll()
        {
            return await _context.AssignmentHistory
                .Include(a => a.Assignment)
                .ToListAsync();
        }

        public async Task<AssignmentHistory> GetByIdAsync(int id)
        {
            return await _context.AssignmentHistory
                .Include(a => a.Assignment)
                .FirstOrDefaultAsync(ah => ah.AssignmentHistoryId == id);
        }

        public async Task<AssignmentStatus> GetLastAssignmentStatus(int id)
        {
            return await _context.Assignment.Where(a => a.AssignmentId == id)
                .SelectMany(a => a.AssignmentHistories)
                .OrderByDescending(ah => ah.CreatedAt)
                .Select(ah => ah.AssignmentStatus)
                .FirstOrDefaultAsync();
        }
    }
}
