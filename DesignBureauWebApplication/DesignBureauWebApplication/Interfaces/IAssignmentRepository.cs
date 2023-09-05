using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAll();
        Task<IEnumerable<Assignment>> GetAllPlusData();

        Task<Assignment?> GetByIdAsync(int id);
        Task<Assignment?> GetByIdPlusDataAsync(int id);

        bool Add(Assignment assignment);

        bool Update(Assignment assignment);

        bool Delete(Assignment assignment);

        bool Save();
    }
}
