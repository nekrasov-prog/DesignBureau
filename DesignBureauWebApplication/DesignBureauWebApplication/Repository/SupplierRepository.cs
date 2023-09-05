using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Supplier Supplier)
        {
            _context.Add(Supplier);
            return Save();
        }

        public bool Update(Supplier supplier)
        {
            _context.Update(supplier);
            return Save();
        }

        public bool Delete(Supplier supplier)
        {
            _context.Remove(supplier);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _context.Supplier.ToListAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            return await _context.Supplier.FirstOrDefaultAsync(s => s.SupplierId == id);
        }
    }
}
