using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Equipment Equipment)
        {
            _context.Add(Equipment);
            return Save();
        }
        public async Task<bool> AddAsync(Equipment Equipment)
        {
            await _context.AddAsync(Equipment);
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Update(Equipment Equipment)
        {
            _context.Update(Equipment);
            return Save();
        }

        public bool Delete(Equipment Equipment)
        {
            _context.Remove(Equipment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Equipment>> GetAll()
        {
            return await _context.Equipment.Include(ed => ed.EquipmentDictionary).ToListAsync();
        }
        public async Task<IEnumerable<Equipment>> GetAllPlusData()
        {
            return await _context.Equipment
                .Include(ed => ed.EquipmentDictionary)
                .Include(i => i.Inventory)
                    .ThenInclude(l => l.Location)
                .Include(eh => eh.EquipmentHistories)
                .ToListAsync();
        }

        public async Task<Equipment> GetByIdAsync(int id)
        {
            return await _context.Equipment.Include(ed => ed.EquipmentDictionary).FirstOrDefaultAsync(e => e.EquipmentId == id);
        }

        public async Task<Equipment> GetByIdPlusDataAsync(int id)
        {
            return await _context.Equipment
                .Include(eh => eh.EquipmentHistories)
                .Include(i => i.Inventory)
                    .ThenInclude(l => l.Location)
                .Include(ed => ed.EquipmentDictionary)
                .SingleOrDefaultAsync(e => e.EquipmentId == id);
        }

        public async Task<List<EquipmentDictionary>> GetAllEquipmentDictionaries()
        {
            return await _context.EquipmentDictionary.ToListAsync();
        }
    }
}
