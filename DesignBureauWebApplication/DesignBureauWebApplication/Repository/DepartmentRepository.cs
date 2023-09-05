//using DesignBureauWebApplication.Data;
//using DesignBureauWebApplication.Interfaces;
//using DesignBureauWebApplication.Models;
//using Microsoft.EntityFrameworkCore;

//namespace DesignBureauWebApplication.Repository
//{
//    public class DepartmentRepository : IDepartmentRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public DepartmentRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public bool Add(Department department)
//        {
//            _context.Add(department);
//            return Save();
//        }

//        public bool Update(Department department)
//        {
//            _context.Update(department);
//            return Save();
//        }

//        public bool Delete(Department department)
//        {
//            _context.Remove(department);
//            return Save();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//        public async Task<IEnumerable<Department>> GetAll()
//        {
//            return await _context.Department.Include(l => l.Location).ToListAsync();
//        }

//        public async Task<Department> GetByIdAsync(int id)
//        {
//            return await _context.Department.Include(l => l.Location).FirstOrDefaultAsync(d => d.DepartmentId == id);
//        }
//    }
//}
