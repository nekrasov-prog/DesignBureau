using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface IConsumptionRepository
    {
        Task<IEnumerable<Consumption>> GetAll();
        Task<Consumption?> GetByIdAsync(int id);

        bool Add(Consumption Consumption);

        bool Update(Consumption Consumption);

        bool Delete(Consumption Consumption);

        bool Save();
    }
}
