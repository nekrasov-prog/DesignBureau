using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAll();
        Task<IEnumerable<Equipment>> GetAllPlusData();

        Task<Equipment?> GetByIdAsync(int id);
        Task<Equipment> GetByIdPlusDataAsync(int id);
        Task<List<EquipmentDictionary>> GetAllEquipmentDictionaries();

        bool Add(Equipment Equipment);
        Task<bool> AddAsync(Equipment Equipment);

        bool Update(Equipment Equipment);

        bool Delete(Equipment Equipment);

        bool Save();
    }
}
