using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class TransportationHistoryRepository : ITransportationHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public TransportationHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(TransportationHistory TransportationHistory)
        {
            _context.Add(TransportationHistory);
            return Save();
        }

        public bool Update(TransportationHistory TransportationHistory)
        {
            _context.Update(TransportationHistory);
            return Save();
        }

        public bool Delete(TransportationHistory TransportationHistory)
        {
            _context.Remove(TransportationHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<TransportationHistory>> GetAll()
        {
            return await _context.TransportationHistory.Include(t => t.Transportation).ToListAsync();
        }

        public async Task<TransportationHistory> GetByIdAsync(int id)
        {
            return await _context.TransportationHistory.Include(t => t.Transportation).FirstOrDefaultAsync(th => th.TransportationHistoryId == id);
        }

        public async Task<TransportationStatus> GetLastTransportationStatus(int id)
        {
            return await _context.Transportation.Where(t => t.TransportationId == id)
                .SelectMany(t => t.TransportationHistories)
                .OrderByDescending(th => th.CreatedAt)
                .Select(th => th.TransportationStatus)
                .FirstOrDefaultAsync();
        }
    }
}
