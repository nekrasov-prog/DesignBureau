//using DesignBureauWebApplication.Data;
//using DesignBureauWebApplication.Interfaces;
//using DesignBureauWebApplication.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DesignBureauWebApplication.Repository
//{
//    public class InventoryOrderRepository : IInventoryOrderRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public InventoryOrderRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public bool Add(InventoryOrder inventoryOrder)
//        {
//            _context.Add(inventoryOrder);
//            return Save();
//        }

//        public bool Update(InventoryOrder inventoryOrder)
//        {
//            _context.Update(inventoryOrder);
//            return Save();
//        }

//        public bool Delete(InventoryOrder inventoryOrder)
//        {
//            _context.Remove(inventoryOrder);
//            return Save();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public async Task<IEnumerable<InventoryOrder>> GetAll()
//        {
//            return await _context.InventoryOrder
//                .Include(i => i.Inventory)
//                .Include(o => o.Order)
//                .ToListAsync();
//        }

//        public async Task<InventoryOrder> GetByIdAsync(int id)
//        {
//            return await _context.InventoryOrder
//                .Include(i => i.Inventory)
//                .Include(o => o.Order)
//                .FirstOrDefaultAsync(io => io.InventoryOrderId == id);
//        }
//    }
//}
