using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Order Order)
        {
            _context.Add(Order);
            return Save();
        }

        public bool Update(Order Order)
        {
            _context.Update(Order);
            return Save();
        }

        public bool Delete(Order Order)
        {
            _context.Remove(Order);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Order.Include(s => s.Supplier).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllPlusData()
        {
            return await _context.Order
                .Include(s => s.Supplier)
                .Include(oh => oh.OrderHistories)
                .Include(oi => oi.OrderItems)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(m => m.Material)
                            .ThenInclude(md => md.MaterialDictionary)
                .Include(oi => oi.OrderItems)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(e => e.Equipment)
                            .ThenInclude(ed => ed.EquipmentDictionary)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Order.Include(s => s.Supplier).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> GetByIdPlusDataAsync(int id)
        {
            return await _context.Order
                .Include(s => s.Supplier)
                .Include(oh => oh.OrderHistories)
                .Include(oi => oi.OrderItems)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(m => m.Material)
                            .ThenInclude(md => md.MaterialDictionary)
                .Include(oi => oi.OrderItems)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(e => e.Equipment)
                            .ThenInclude(ed => ed.EquipmentDictionary)
                .Include(oi => oi.OrderItems)
                     .ThenInclude(i => i.Inventory)
                            .ThenInclude(m => m.InventoryTransportations)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await _context.Supplier.ToListAsync();
        }

    }
}
