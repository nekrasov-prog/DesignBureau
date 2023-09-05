using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IInventoryTransportationHistoryRepository
    {
        Task<IEnumerable<InventoryTransportationHistory>> GetAll();

        Task<InventoryTransportationHistory?> GetByIdAsync(int id);

        bool Add(InventoryTransportationHistory InventoryTransportationHistory);

        bool Update(InventoryTransportationHistory InventoryTransportationHistory);

        bool Delete(InventoryTransportationHistory InventoryTransportationHistory);

        bool Save();
    }
}
