using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IInventoryTransportationRepository
    {
        Task<IEnumerable<InventoryTransportation>> GetAll();

        Task<InventoryTransportation?> GetByIdAsync(int id);

        bool Add(InventoryTransportation inventoryTransportation);

        bool Update(InventoryTransportation inventoryTransportation);

        bool Delete(InventoryTransportation inventoryTransportation);

        bool Save();
    }
}
