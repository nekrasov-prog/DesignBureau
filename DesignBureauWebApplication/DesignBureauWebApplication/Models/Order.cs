using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(512)]
        public string ContractNumber { get; set; }

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public List<OrderHistory> OrderHistories { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        //[InverseProperty("Order")]
        //public virtual ICollection<InventoryOrder> InventoryOrders { get; set; }
    }
}
