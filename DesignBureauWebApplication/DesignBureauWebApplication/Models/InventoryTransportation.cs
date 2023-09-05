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
    public class InventoryTransportation
    {
        [Key]
        public int InventoryTransportationId { get; set; }
        [Required]
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        [Required]
        [ForeignKey("Transportation")]
        public int TransportationId { get; set; }
        public Inventory Inventory { get; set; }
        public Transportation Transportation { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
