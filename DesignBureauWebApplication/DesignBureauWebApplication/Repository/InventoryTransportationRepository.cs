using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class InventoryTransportationRepository : IInventoryTransportationRepository
    {
        private readonly ApplicationDbContext _context;
        public InventoryTransportationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(InventoryTransportation inventoryTransportation)
        {
            _context.Add(inventoryTransportation);
            return Save();
        }

        public bool Update(InventoryTransportation inventoryTransportation)
        {
            _context.Update(inventoryTransportation);
            return Save();
        }

        public bool Delete(InventoryTransportation inventoryTransportation)
        {
            _context.Remove(inventoryTransportation);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<InventoryTransportation>> GetAll()
        {
            return await _context.InventoryTransportation
                .Include(i => i.Inventory)
                    .ThenInclude(m => m.Material)
                        .ThenInclude(md => md.MaterialDictionary)
                .Include(i => i.Inventory)
                    .ThenInclude(e => e.Equipment)
                        .ThenInclude(ed => ed.EquipmentDictionary)
                .Include(t => t.Transportation)
                .ToListAsync();
        }

        public async Task<InventoryTransportation> GetByIdAsync(int id)
        {
            return await _context.InventoryTransportation
                .Include(i => i.Inventory)
                    .ThenInclude(m => m.Material)
                        .ThenInclude(md => md.MaterialDictionary)
                .Include(i => i.Inventory)
                    .ThenInclude(e => e.Equipment)
                        .ThenInclude(ed => ed.EquipmentDictionary)
                .Include(t => t.Transportation)
                .FirstOrDefaultAsync(it => it.InventoryTransportationId == id);
        }
    }
}
