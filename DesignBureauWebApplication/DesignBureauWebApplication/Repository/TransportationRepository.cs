using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignBureauWebApplication.Repository
{
    public class TransportationRepository : ITransportationRepository
    {
        private readonly ApplicationDbContext _context;
        public TransportationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Transportation transportation)
        {
            _context.Add(transportation);
            return Save();
        }

        public bool Update(Transportation transportation)
        {
            _context.Update(transportation);
            return Save();
        }

        public bool Delete(Transportation transportation)
        {
            _context.Remove(transportation);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Transportation>> GetAll()
        {
            return await _context.Transportation
                .Include(o => o.Origin)
                .Include(d => d.Destination)
                .Include(th => th.TransportationHistories)
                .Include(it => it.InventoryTransportations)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(m => m.Material)
                            .ThenInclude(md => md.MaterialDictionary)
                .Include(it => it.InventoryTransportations)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(m => m.Equipment)
                            .ThenInclude(md => md.EquipmentDictionary)
                .ToListAsync();
        }

        public async Task<Transportation> GetByIdAsync(int id)
        {
            return await _context.Transportation
                .Include(o => o.Origin)
                .Include(d => d.Destination)
                .Include(th => th.TransportationHistories)
                .Include(it => it.InventoryTransportations)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(m => m.Material)
                            .ThenInclude(md => md.MaterialDictionary)
                .Include(it => it.InventoryTransportations)
                    .ThenInclude(i => i.Inventory)
                        .ThenInclude(m => m.Equipment)
                            .ThenInclude(md => md.EquipmentDictionary)
                .FirstOrDefaultAsync(t => t.TransportationId == id);
        }

        public async Task<int> GetOrderByTransportation(int transportationId)
        {
            var orderItem = await _context.OrderItem
                .Include(oi => oi.Order)
                .Include(oi => oi.Inventory)
                    .ThenInclude(i => i.InventoryTransportations)
                        .ThenInclude(it => it.Transportation)
                .Where(oi => oi.Inventory.InventoryTransportations.Any(it => it.TransportationId == transportationId))
                .FirstOrDefaultAsync();

            return orderItem.OrderId;
        }

    }
}
