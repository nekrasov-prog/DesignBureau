using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _context;
        public PositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Position position)
        {
            _context.Add(position);
            return Save();
        }

        public bool Update(Position position)
        {
            _context.Update(position);
            return Save();
        }

        public bool Delete(Position position)
        {
            _context.Remove(position);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Position>> GetAll()
        {
            return await _context.Position.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await _context.Position.FirstOrDefaultAsync(p => p.PositionId == id);
        }
    }
}
