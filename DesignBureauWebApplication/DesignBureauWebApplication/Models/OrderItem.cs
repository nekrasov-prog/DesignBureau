using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }

        [Required]
        public int Cost { get; set; }

        public QualityCheckingGrade? QualityCheckingGrade { get; set; }

    }
}
