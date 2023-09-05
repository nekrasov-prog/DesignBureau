using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;
        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Inventory inventory)
        {
            _context.Add(inventory);
            return Save();
        }

        public bool Update(Inventory inventory)
        {
            _context.Update(inventory);
            return Save();
        }

        public bool Delete(Inventory inventory)
        {
            _context.Remove(inventory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Inventory>> GetAll()
        {
            return await _context.Inventory
                .Include(m => m.Material)
                    .ThenInclude(md => md.MaterialDictionary)
                .Include(e => e.Equipment)
                    .ThenInclude(ed => ed.EquipmentDictionary)
                .Include(l => l.Location)
                .Include(it => it.InventoryTransportations)
                .ToListAsync();
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            return await _context.Inventory
                .Include(m => m.Material)
                .Include(e => e.Equipment)
                .FirstOrDefaultAsync(i => i.InventoryId == id);
        }

        public async Task<Inventory> GetInventoryByEquipmentId(int id)
        {
            var inventories = await _context.Inventory
                .Include(e => e.Equipment)
                .ToListAsync();
            return inventories.FirstOrDefault(i => i.EquipmentId == id);
        }

        public async Task<Inventory> GetInventoryByMaterialId(int id)
        {
            var inventories = await _context.Inventory
                .Include(e => e.Material)
                .ToListAsync();
            return inventories.FirstOrDefault(i => i.MaterialId == id);
        }

        public async Task<Inventory> GetByMaterialNameAndLocation(string materialName, int? locationId)
        {
            return await _context.Inventory
                .Include(i => i.Material)
                    .ThenInclude(m => m.MaterialDictionary)
                .Include(i => i.Location)
                .FirstOrDefaultAsync(i => i.Material.MaterialDictionary.MaterialName == materialName && i.LocationId == locationId);
        }
    }
}
