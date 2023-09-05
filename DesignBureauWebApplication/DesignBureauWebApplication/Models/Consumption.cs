using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class Consumption
    {
        [Key]
        public int ConsumptionId { get; set; }

        [Required]
        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        [Required]
        [ForeignKey("Material")]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        [Required]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
