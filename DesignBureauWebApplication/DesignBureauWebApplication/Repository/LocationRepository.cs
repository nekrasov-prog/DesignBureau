using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Location location)
        {
            _context.Add(location);
            return Save();
        }

        public bool Delete(Location location)
        {
            _context.Remove(location);
            return Save();
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Location.ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _context.Location.FirstOrDefaultAsync(l => l.LocationId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Location location)
        {
            _context.Update(location);
            return Save();
        }
    }
}
