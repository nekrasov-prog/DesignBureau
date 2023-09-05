using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(OrderHistory OrderHistory)
        {
            _context.Add(OrderHistory);
            return Save();
        }

        public bool Update(OrderHistory OrderHistory)
        {
            _context.Update(OrderHistory);
            return Save();
        }

        public bool Delete(OrderHistory OrderHistory)
        {
            _context.Remove(OrderHistory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<OrderHistory>> GetAll()
        {
            return await _context.OrderHistory.Include(o => o.Order).ToListAsync();
        }

        public async Task<OrderHistory> GetByIdAsync(int id)
        {
            return await _context.OrderHistory.Include(o => o.Order).FirstOrDefaultAsync(oh => oh.OrderHistoryId == id);
        }

        public async Task<OrderStatus> GetLastOrderStatus(int id)
        {
            return await _context.Order.Where(o => o.OrderId == id)
                .SelectMany(o => o.OrderHistories)
                .OrderByDescending(wh => wh.CreatedAt)
                .Select(oh => oh.OrderStatus)
                .FirstOrDefaultAsync();
        }
    }
}
