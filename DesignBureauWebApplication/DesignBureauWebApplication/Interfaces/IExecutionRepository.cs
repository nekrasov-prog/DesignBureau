using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IExecutionRepository
    {
        Task<IEnumerable<Execution>> GetAll();

        Task<Execution?> GetByIdAsync(int id);

        bool Add(Execution Execution);

        bool Update(Execution Execution);

        bool Delete(Execution Execution);

        bool Save();

        Task<Execution> FindByAssignmentIdAndEmployeeIdAsync(int assignmentId, int employeeId);
    }
}
