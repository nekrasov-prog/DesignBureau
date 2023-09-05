using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string ContractNumber { get; set; }
        public int SupplierId { get; set; }
        //public string SName { get; set; }
        public SelectList? SupplierList { get; set; }
        //public Supplier Supplier { get; set; }
        //public Order Order { get; set; }
        //public List<Supplier> Suppliers { get; set; }
    }
}
