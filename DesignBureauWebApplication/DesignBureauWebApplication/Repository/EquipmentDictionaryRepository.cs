using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class EquipmentDictionaryRepository : IEquipmentDictionaryRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipmentDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(EquipmentDictionary EquipmentDictionary)
        {
            _context.Add(EquipmentDictionary);
            return Save();
        }

        public bool Update(EquipmentDictionary EquipmentDictionary)
        {
            _context.Update(EquipmentDictionary);
            return Save();
        }

        public bool Delete(EquipmentDictionary EquipmentDictionary)
        {
            _context.Remove(EquipmentDictionary);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<EquipmentDictionary>> GetAll()
        {
            return await _context.EquipmentDictionary.ToListAsync();
        }

        public async Task<EquipmentDictionary> GetByIdAsync(int id)
        {
            return await _context.EquipmentDictionary.FirstOrDefaultAsync(ed => ed.EquipmentDictionaryId == id);
        }
    }
}
