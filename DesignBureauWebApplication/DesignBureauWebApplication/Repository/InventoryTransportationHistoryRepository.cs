using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class InventoryTransportationHistoryRepository : IInventoryTransportationHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public InventoryTransportationHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(InventoryTransportationHistory InventoryTransportationHistory)
        {
            _context.Add(InventoryTransportationHistory);
            return Save();
        }

        public bool Update(InventoryTransportationHistory InventoryTransportationHistory)
        {
            _context.Update(InventoryTransportationHistory);
            return Save();
        }

        public bool Delete(InventoryTransportationHistory InventoryTransportationHistory)
        {
            _context.Remove(InventoryTransportationHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<InventoryTransportationHistory>> GetAll()
        {
            return await _context.InventoryTransportationHistory
                .Include(it => it.InventoryTransportation)
                .ToListAsync();
        }

        public async Task<InventoryTransportationHistory> GetByIdAsync(int id)
        {
            return await _context.InventoryTransportationHistory
                .Include(it => it.InventoryTransportation)
                .FirstOrDefaultAsync(ith => ith.InventoryTransportationHistoryId == id);
        }
    }
}
