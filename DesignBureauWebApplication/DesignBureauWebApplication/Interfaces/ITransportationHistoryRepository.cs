using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface ITransportationHistoryRepository
    {
        Task<IEnumerable<TransportationHistory>> GetAll();

        Task<TransportationHistory?> GetByIdAsync(int id);

        bool Add(TransportationHistory TransportationHistory);

        bool Update(TransportationHistory TransportationHistory);

        bool Delete(TransportationHistory TransportationHistory);

        bool Save();

        Task<TransportationStatus> GetLastTransportationStatus(int id);
    }
}
