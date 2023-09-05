using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAll();
        Task<IEnumerable<Material>> GetAllPlusData();
        Task<List<MaterialDictionary>> GetAllMaterialDictionaries();

        Task<Material?> GetByIdAsync(int id);
        Task<Material> GetByIdPlusDataAsync(int id);

        bool Add(Material Material);

        bool Update(Material Material);

        bool Delete(Material Material);

        bool Save();

        Task<MaterialDictionary> GetMaterialDictionaryById(int id);
        Task<Material> GetMaterialByDictionaryAndLocation(int materialDictionaryId, int? locationId);
        Task<bool> IsOrderedAsync(int materialId);
        Task<Order> GetOrderByMaterial(int materialId);
    }
}
