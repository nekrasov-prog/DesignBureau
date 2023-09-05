using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsOrderViewModel
    {
        public int OrderId { get; set; }
        public string ContractNumber { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public List<OrderHistory> OrderHistories { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus LastOrderStatus { get; set; }
    }
}
