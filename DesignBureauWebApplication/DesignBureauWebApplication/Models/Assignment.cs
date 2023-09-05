using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        [ForeignKey("AssignmentDictionary")]
        public int AssignmentDictionaryId { get; set; }
        public virtual AssignmentDictionary AssignmentDictionary { get; set; }

        [Required]
        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        public List<AssignmentHistory> AssignmentHistories { get; set; }
        public List<Consumption> Consumptions { get; set; }
        public List<Execution> Executions { get; set; }
    }
}
