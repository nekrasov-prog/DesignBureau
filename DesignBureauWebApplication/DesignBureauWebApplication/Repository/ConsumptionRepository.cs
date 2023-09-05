using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private readonly ApplicationDbContext _context;
        public ConsumptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Consumption Consumption)
        {
            _context.Add(Consumption);
            return Save();
        }

        public bool Update(Consumption Consumption)
        {
            _context.Update(Consumption);
            return Save();
        }

        public bool Delete(Consumption Consumption)
        {
            _context.Remove(Consumption);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Consumption>> GetAll()
        {
            return await _context.Consumption
                .Include(a => a.Assignment)
                .Include(m => m.Material)
                .ToListAsync();
        }

        public async Task<Consumption> GetByIdAsync(int id)
        {
            return await _context.Consumption
                .Include(a => a.Assignment)
                .Include(m => m.Material)
                .FirstOrDefaultAsync(c => c.ConsumptionId == id);
        }
    }
}
