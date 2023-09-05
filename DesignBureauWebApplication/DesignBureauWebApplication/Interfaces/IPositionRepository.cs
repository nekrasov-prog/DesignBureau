using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetAll();

        Task<Position?> GetByIdAsync(int id);

        bool Add(Position position);

        bool Update(Position position);

        bool Delete(Position position);

        bool Save();
    }
}
