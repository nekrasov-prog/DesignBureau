using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IMaterialDictionaryRepository
    {
        Task<IEnumerable<MaterialDictionary>> GetAll();

        Task<MaterialDictionary?> GetByIdAsync(int id);

        bool Add(MaterialDictionary MaterialDictionary);

        bool Update(MaterialDictionary MaterialDictionary);

        bool Delete(MaterialDictionary MaterialDictionary);

        bool Save();
    }
}
