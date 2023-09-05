using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(OrderItem OrderItem)
        {
            _context.Add(OrderItem);
            return Save();
        }

        public bool Update(OrderItem OrderItem)
        {
            _context.Update(OrderItem);
            return Save();
        }

        public bool Delete(OrderItem OrderItem)
        {
            _context.Remove(OrderItem);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await _context.OrderItem
                .Include(o => o.Order)
                .Include(i => i.Inventory)
                .ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItem
                .Include(o => o.Order)
                .Include(i => i.Inventory)
                .FirstOrDefaultAsync(oi => oi.OrderItemId == id);
        }

        public async Task<OrderItem> GetByIdPlusDataAsync(int id)
        {
            return await _context.OrderItem
                .Include(o => o.Order)
                .Include(i => i.Inventory)
                    .ThenInclude(m => m.Material)
                        .ThenInclude(md => md.MaterialDictionary)
                 .Include(i => i.Inventory)
                    .ThenInclude(e => e.Equipment)
                        .ThenInclude(ed => ed.EquipmentDictionary)
                .SingleOrDefaultAsync(oi => oi.OrderItemId == id);
        }

        public async Task<List<EquipmentDictionary>> GetAllEquipmentDictionaries()
        {
            return await _context.EquipmentDictionary.ToListAsync();
        }

        public async Task<List<MaterialDictionary>> GetAllMaterialDictionaries()
        {
            return await _context.MaterialDictionary.ToListAsync();
        }

        public async Task<bool> CheckMaterialExistsInOrderAsync(int orderId, int materialDictionaryId)
        {
            return await _context.OrderItem
                .Include(oi => oi.Inventory.Material.MaterialDictionary)
                .AnyAsync(oi => oi.OrderId == orderId && oi.Inventory.Material.MaterialDictionaryId == materialDictionaryId);
        }

        public async Task<OrderItem> GetOrderItemByMaterialAsync(int orderId, int materialDictionaryId)
        {
            return await _context.OrderItem
                .Include(oi => oi.Inventory)
                    .ThenInclude(inv => inv.Material)
                        .ThenInclude(mat => mat.MaterialDictionary)
                .SingleOrDefaultAsync(oi => oi.OrderId == orderId &&
                    oi.Inventory.Material.MaterialDictionaryId == materialDictionaryId);
        }

        public async Task AddQuantityToMaterialInOrderAsync(int orderId, int materialDictionaryId, int quantityToAdd)
        {
            var orderItem = await _context.OrderItem
                .Include(oi => oi.Inventory)
                .ThenInclude(inv => inv.Material)
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.Inventory.Material.MaterialDictionaryId == materialDictionaryId);

            if (orderItem == null)
            {
                throw new ArgumentException($"There is no order item for order {orderId} and material {materialDictionaryId}");
            }

            orderItem.Inventory.Material.Quantity += quantityToAdd;

            _context.Entry(orderItem.Inventory.Material).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
