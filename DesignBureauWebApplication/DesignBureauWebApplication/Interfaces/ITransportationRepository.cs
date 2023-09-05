using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface ITransportationRepository
    {
        Task<IEnumerable<Transportation>> GetAll();

        Task<Transportation?> GetByIdAsync(int id);

        bool Add(Transportation transportation);

        bool Update(Transportation transportation);

        bool Delete(Transportation transportation);

        bool Save();

        Task<int> GetOrderByTransportation(int transportationId);
    }
}
