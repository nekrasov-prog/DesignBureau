using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class InventoryTransportationHistory
    {
        [Key]
        public int InventoryTransportationHistoryId { get; set; }

        [Required]
        [ForeignKey("InventoryTransportation")]
        public int InventoryTransportationId { get; set; }
        public virtual InventoryTransportation InventoryTransportation { get; set; }

        [Required]
        public InventoryTransportationStatus InventoryTransportationStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
