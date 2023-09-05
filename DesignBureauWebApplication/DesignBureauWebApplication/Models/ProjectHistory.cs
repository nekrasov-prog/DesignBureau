using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class ProjectHistory
    {
        [Key]
        public int ProjectHistoryId { get; set; }

        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        public ProjectStatus ProjectStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
