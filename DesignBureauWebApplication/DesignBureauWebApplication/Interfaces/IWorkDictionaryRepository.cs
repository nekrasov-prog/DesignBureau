using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IWorkDictionaryRepository
    {
        Task<IEnumerable<WorkDictionary>> GetAll();

        Task<WorkDictionary?> GetByIdAsync(int id);

        bool Add(WorkDictionary workDictionary);

        bool Update(WorkDictionary workDictionary);

        bool Delete(WorkDictionary workDictionary);

        bool Save();
    }
}
