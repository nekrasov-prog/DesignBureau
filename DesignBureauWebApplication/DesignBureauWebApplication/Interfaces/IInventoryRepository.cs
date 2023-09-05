using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAll();

        Task<Inventory?> GetByIdAsync(int id);

        bool Add(Inventory inventory);

        bool Update(Inventory inventory);

        bool Delete(Inventory inventory);

        bool Save();

        Task<Inventory> GetInventoryByEquipmentId(int id);
        Task<Inventory> GetInventoryByMaterialId(int id);
        Task<Inventory> GetByMaterialNameAndLocation(string materialName, int? locationId);

    }
}
