using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IOrderHistoryRepository
    {
        Task<IEnumerable<OrderHistory>> GetAll();
        Task<OrderHistory?> GetByIdAsync(int id);
        bool Add(OrderHistory OrderHistory);
        bool Update(OrderHistory OrderHistory);
        bool Delete(OrderHistory OrderHistory);
        bool Save();
        Task<OrderStatus> GetLastOrderStatus(int id);
    }
}
