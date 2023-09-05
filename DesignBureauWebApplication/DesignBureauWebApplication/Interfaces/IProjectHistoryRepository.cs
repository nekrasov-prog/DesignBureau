using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.Data.Enum;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IProjectHistoryRepository
    {
        Task<IEnumerable<ProjectHistory>> GetAll();

        Task<ProjectHistory?> GetByIdAsync(int id);

        bool Add(ProjectHistory ProjectHistory);

        bool Update(ProjectHistory ProjectHistory);

        bool Delete(ProjectHistory ProjectHistory);

        bool Save();

        Task<ProjectStatus> GetLastProjectStatus(int id);

        Task<ProjectStatus> GetPreviousProjectStatus(int id);
    }
}
