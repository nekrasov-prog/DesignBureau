using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IAssignmentDictionaryRepository
    {
        Task<IEnumerable<AssignmentDictionary>> GetAll();

        Task<AssignmentDictionary?> GetByIdAsync(int id);

        bool Add(AssignmentDictionary AssignmentDictionary);

        bool Update(AssignmentDictionary AssignmentDictionary);

        bool Delete(AssignmentDictionary AssignmentDictionary);

        bool Save();
    }
}
