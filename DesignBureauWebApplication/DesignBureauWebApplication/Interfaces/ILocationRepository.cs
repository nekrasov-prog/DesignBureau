using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();

        Task<Location?> GetByIdAsync(int id);

        bool Add(Location location);

        bool Update(Location location);

        bool Delete(Location location);

        bool Save();
    }
}
