using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool Update(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }

        public bool Delete(Employee employee)
        {
            _context.Remove(employee);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employee.Include(p => p.Position).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employee.Include(p => p.Position).FirstOrDefaultAsync(e => e.EmployeeId == id);
        }
    }
}
