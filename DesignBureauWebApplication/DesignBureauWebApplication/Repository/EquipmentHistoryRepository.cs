using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class EquipmentHistoryRepository : IEquipmentHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipmentHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(EquipmentHistory EquipmentHistory)
        {
            _context.Add(EquipmentHistory);
            return Save();
        }

        public bool Update(EquipmentHistory EquipmentHistory)
        {
            _context.Update(EquipmentHistory);
            return Save();
        }

        public bool Delete(EquipmentHistory EquipmentHistory)
        {
            _context.Remove(EquipmentHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<EquipmentHistory>> GetAll()
        {
            return await _context.EquipmentHistory
                .Include(e => e.Equipment)
                .ToListAsync();
        }

        public async Task<EquipmentHistory> GetByIdAsync(int id)
        {
            return await _context.EquipmentHistory
                .Include(e => e.Equipment)
                .FirstOrDefaultAsync(eh => eh.EquipmentHistoryId == id);
        }

        public async Task<EquipmentStatus> GetActualEquipmentStatus(int id)
        {
            var now = DateTime.Now;
            return await _context.Equipment
                .Where(e => e.EquipmentId == id)
                .SelectMany(e => e.EquipmentHistories)
                .Where(eh => eh.StatusStart <= now && (eh.StatusEnd == null || eh.StatusEnd >= now))
                .OrderByDescending(eh => eh.StatusStart)
                .Select(eh => eh.EquipmentStatus)
                .FirstOrDefaultAsync();
        }
    }
}
