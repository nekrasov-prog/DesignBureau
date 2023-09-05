using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();

        Task<Project?> GetByIdAsync(int id);
        Task<Project> GetByIdPlusDataAsync(int id);

        bool Add(Project project);

        bool Update(Project project);

        bool Delete(Project project);

        bool Save();

        Task<int> GetCompletedWorksCountAsync(int projectId);

        Task<int> GetTotalWorksCount(int projectId);
    }
}
