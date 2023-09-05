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
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [ForeignKey("Material")]
        public int? MaterialId { get; set; }
        public virtual Material? Material { get; set; }

        [ForeignKey("Equipment")]
        public int? EquipmentId { get; set; }
        public virtual Equipment? Equipment { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public virtual Location? Location { get; set; }

        //[InverseProperty("Inventory")]
        //public virtual ICollection<Interaction> Interactions { get; set; }

        //[InverseProperty("Inventory")]
        public virtual ICollection<InventoryTransportation>? InventoryTransportations { get; set; }

        //[InverseProperty("Inventory")]
        //public virtual ICollection<InventoryOrder> InventoryOrders { get; set; }
    }

}
