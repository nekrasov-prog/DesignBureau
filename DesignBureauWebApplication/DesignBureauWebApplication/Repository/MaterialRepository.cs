using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;
        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Material Material)
        {
            _context.Add(Material);
            return Save();
        }

        public bool Delete(Material Material)
        {
            _context.Remove(Material);
            return Save();
        }

        public async Task<IEnumerable<Material>> GetAll()
        {
            return await _context.Material
                .Include(md => md.MaterialDictionary)
                .ToListAsync();
        }
        public async Task<IEnumerable<Material>> GetAllPlusData()
        {
            return await _context.Material
                .Include(md => md.MaterialDictionary)
                .Include(i => i.Inventory)
                    .ThenInclude(l => l.Location)
                .Include(c => c.Consumptions)
                    .ThenInclude(a => a.Assignment)
                        .ThenInclude(ad => ad.AssignmentDictionary)
                .ToListAsync();
        }

        public async Task<Material?> GetByIdAsync(int id)
        {
            return await _context.Material
                .Include(md => md.MaterialDictionary)
                .FirstOrDefaultAsync(m => m.MaterialId == id);
        }
        public async Task<Material?> GetByIdPlusDataAsync(int id)
        {
            return await _context.Material
                .Include(md => md.MaterialDictionary)
                .Include(i => i.Inventory)
                    .ThenInclude(l => l.Location)
                .Include(c => c.Consumptions)
                    .ThenInclude(a => a.Assignment)
                        .ThenInclude(ad => ad.AssignmentDictionary)
                .SingleOrDefaultAsync(m => m.MaterialId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Material Material)
        {
            _context.Update(Material);
            return Save();
        }

        public async Task<List<MaterialDictionary>> GetAllMaterialDictionaries()
        {
            return await _context.MaterialDictionary.ToListAsync();
        }

        public async Task<MaterialDictionary> GetMaterialDictionaryById(int id)
        {
            return await _context.MaterialDictionary.FindAsync(id);
        }

        public async Task<Material> GetMaterialByDictionaryAndLocation(int materialDictionaryId, int? locationId)
        {
            var material = await _context.Material
                .Include(m => m.Inventory)
                .FirstOrDefaultAsync(m => m.MaterialDictionaryId == materialDictionaryId && m.Inventory.LocationId == locationId);

            return material;
        }

        public async Task<bool> IsOrderedAsync(int materialId)
        {
            var orderId = _context.OrderItem
                .AsNoTracking()
                .Where(oi => oi.Inventory.Material.MaterialId == materialId)
                .Select(oi => oi.Order.OrderId)
                .FirstOrDefault();
            string? lastStatus = "";
            if (orderId != null)
            {
                var orderStatus = await _context.Order.Where(o => o.OrderId == orderId)
                    .SelectMany(o => o.OrderHistories)
                    .OrderByDescending(wh => wh.CreatedAt)
                    .Select(oh => oh.OrderStatus)
                    .FirstOrDefaultAsync();
                if (orderStatus != null)
                {
                    lastStatus = orderStatus.ToString();
                }
            }
            if (lastStatus != OrderStatus.Delivered.ToString())
            {
                return true;
            }
            return false;
        }

        public async Task<Order> GetOrderByMaterial(int materialId)
        {
            var order = await _context.OrderItem
                .AsNoTracking()
                .Where(oi => oi.Inventory.Material.MaterialId == materialId)
                .Select(oi => oi.Order)
                .FirstOrDefaultAsync();
            return order;
        }
    }
}
