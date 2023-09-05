using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class OrderHistory
    {
        [Key]
        public int OrderHistoryId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
