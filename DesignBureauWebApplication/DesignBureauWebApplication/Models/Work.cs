using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Work
    {
        [Key]
        public int WorkId { get; set; }

        [Required]
        public DateTime PlanStartDate { get; set; }

        [Required]
        public DateTime PlanOverDate { get; set; }

        // Navigation Properties
        public Project Project { get; set; }
        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public WorkDictionary WorkDictionary { get; set; }
        [Required]
        [ForeignKey("WorkDictionary")]
        public int WorkDictionaryId { get; set; }

        public List<Assignment> Assignments { get; set; }
        public List<WorkHistory> WorkHistories { get; set; }

        // Collection Navigation Property    list maybe??
        //[InverseProperty("Work")]
        //public ICollection<Interaction> Interactions { get; set; }
        //[InverseProperty("Work2")]
        //public virtual ICollection<Work> ParentWorks { get; set; }
        //[InverseProperty("Work4")]
        //public virtual ICollection<Work> ChildWorks { get; set; }
    }

}
