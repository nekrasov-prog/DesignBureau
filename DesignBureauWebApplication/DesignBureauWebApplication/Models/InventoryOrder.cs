using DesignBureauWebApplication.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class InventoryOrder
    {
        [Key]
        public int InventoryOrderId { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public DateTime PlanDeliveryDateTime { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        [Required]
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

}
