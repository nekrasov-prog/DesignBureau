using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IEquipmentHistoryRepository
    {
        Task<IEnumerable<EquipmentHistory>> GetAll();

        Task<EquipmentHistory?> GetByIdAsync(int id);

        bool Add(EquipmentHistory EquipmentHistory);

        bool Update(EquipmentHistory EquipmentHistory);

        bool Delete(EquipmentHistory EquipmentHistory);

        bool Save();
        Task<EquipmentStatus> GetActualEquipmentStatus(int id);
    }
}
