using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAll();

        Task<OrderItem?> GetByIdAsync(int id);
        Task<OrderItem> GetByIdPlusDataAsync(int id);

        bool Add(OrderItem OrderItem);

        bool Update(OrderItem OrderItem);

        bool Delete(OrderItem OrderItem);

        bool Save();

        Task<List<EquipmentDictionary>> GetAllEquipmentDictionaries();
        Task<List<MaterialDictionary>> GetAllMaterialDictionaries();
        Task<bool> CheckMaterialExistsInOrderAsync(int orderId, int materialDictionaryId);
        Task<OrderItem> GetOrderItemByMaterialAsync(int orderId, int materialDictionaryId);
        Task AddQuantityToMaterialInOrderAsync(int orderId, int materialDictionaryId, int quantityToAdd);
    }
}
