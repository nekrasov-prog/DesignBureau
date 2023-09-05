using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IAssignmentHistoryRepository
    {
        Task<IEnumerable<AssignmentHistory>> GetAll();

        Task<AssignmentHistory?> GetByIdAsync(int id);

        bool Add(AssignmentHistory AssignmentHistory);

        bool Update(AssignmentHistory AssignmentHistory);

        bool Delete(AssignmentHistory AssignmentHistory);

        bool Save();

        Task<AssignmentStatus> GetLastAssignmentStatus(int id);
    }
}
