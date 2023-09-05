using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DesignBureauWebApplication.Data.Enum;

namespace DesignBureauWebApplication.Models
{
    public class Interaction
    {
        [Key]
        public int InteractionId { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime? CreatedAt { get; set; }

        public InteractionStatus? InteractionStatus { get; set; }

        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }

        [ForeignKey("InteractionDictionary")]
        public int InteractionDictionaryId { get; set; }
        public virtual InteractionDictionary InteractionDictionary { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
    }

}
