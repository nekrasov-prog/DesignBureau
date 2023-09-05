using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetAllPlusData();

        //Task<IEnumerable<Order>> GetSliceAsync(int offset, int size);

        //Task<IEnumerable<Order>> GetOrdersByState(string state);

        //Task<IEnumerable<Order>> GetOrdersByCategoryAndSliceAsync(OrderCategory category, int offset, int size);

        Task<List<Supplier>> GetAllSuppliers();

        //Task<List<City>> GetAllCitiesByState(string state);

        Task<Order?> GetByIdAsync(int id);

        Task<Order> GetByIdPlusDataAsync(int id);

        //Task<Order?> GetByIdAsyncNoTracking(int id);

        //Task<IEnumerable<Order>> GetOrderByCity(string city);

        //Task<int> GetCountAsync();

        //Task<int> GetCountByCategoryAsync(OrderCategory category);

        bool Add(Order Order);

        bool Update(Order Order);

        bool Delete(Order Order);

        bool Save();
    }
}
