using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll();

        Task<Supplier?> GetByIdAsync(int id);

        bool Add(Supplier supplier);

        bool Update(Supplier supplier);

        bool Delete(Supplier supplier);

        bool Save();
    }
}
