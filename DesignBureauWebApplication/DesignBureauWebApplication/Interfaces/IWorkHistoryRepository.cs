using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IWorkHistoryRepository
    {
        Task<IEnumerable<WorkHistory>> GetAll();

        Task<WorkHistory?> GetByIdAsync(int id);

        bool Add(WorkHistory WorkHistory);

        bool Update(WorkHistory WorkHistory);

        bool Delete(WorkHistory WorkHistory);

        bool Save();

        Task<WorkStatus> GetLastWorkStatus(int id);

    }
}
