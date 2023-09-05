using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class WorkHistory
    {
        [Key]
        public int WorkHistoryId { get; set; }

        [Required]
        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }

        [Required]
        public WorkStatus WorkStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
