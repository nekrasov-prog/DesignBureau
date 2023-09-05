using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class ExecutionRepository : IExecutionRepository
    {
        private readonly ApplicationDbContext _context;
        public ExecutionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Execution Execution)
        {
            _context.Add(Execution);
            return Save();
        }

        public bool Update(Execution Execution)
        {
            _context.Update(Execution);
            return Save();
        }

        public bool Delete(Execution Execution)
        {
            _context.Remove(Execution);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Execution>> GetAll()
        {
            return await _context.Execution
                .Include(e => e.Employee)
                .Include(a => a.Assignment)
                .ToListAsync();
        }

        public async Task<Execution> GetByIdAsync(int id)
        {
            return await _context.Execution
                .Include(e => e.Employee)
                    .ThenInclude(e => e.Position)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.AssignmentDictionary)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.Work)
                        .ThenInclude(w => w.WorkDictionary)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.Work)
                        .ThenInclude(w => w.Project)
                .FirstOrDefaultAsync(ex => ex.ExecutionId == id);
        }

        public async Task<Execution> FindByAssignmentIdAndEmployeeIdAsync(int assignmentId, int employeeId)
        {
            return await _context.Execution
                .Include(e => e.Employee)
                .Include(e => e.Employee.Position)
                .Include(e => e.Assignment)
                .FirstOrDefaultAsync(e => e.AssignmentId == assignmentId && e.EmployeeId == employeeId);
        }
    }
}
