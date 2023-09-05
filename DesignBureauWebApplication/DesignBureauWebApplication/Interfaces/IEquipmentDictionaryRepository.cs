using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IEquipmentDictionaryRepository
    {
        Task<IEnumerable<EquipmentDictionary>> GetAll();

        Task<EquipmentDictionary?> GetByIdAsync(int id);

        bool Add(EquipmentDictionary EquipmentDictionary);

        bool Update(EquipmentDictionary EquipmentDictionary);

        bool Delete(EquipmentDictionary EquipmentDictionary);

        bool Save();
    }
}
