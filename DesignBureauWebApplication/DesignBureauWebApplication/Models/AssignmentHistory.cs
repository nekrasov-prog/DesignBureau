using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class AssignmentHistory
    {
        [Key]
        public int AssignmentHistoryId { get; set; }

        [Required]
        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        [Required]
        public AssignmentStatus AssignmentStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
