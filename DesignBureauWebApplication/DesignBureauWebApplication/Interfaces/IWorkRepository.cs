using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IWorkRepository
    {
        Task<IEnumerable<Work>> GetAll();

        Task<Work?> GetByIdAsync(int id);
        Task<Work?> GetByIdPlusDataAsync(int id);
        Task<List<WorkDictionary>> GetAllWorkDictionaries();

        Task<IEnumerable<Work>> GetAllWorksByProject(int projectId);

        bool Add(Work work);

        bool Update(Work work);

        bool Delete(Work work);

        bool Save();

        Task<int> GetCompletedAssignmentsCountAsync(int workId);
        Task<int> GetTotalAssignmentsCount(int workId);
    }
}
