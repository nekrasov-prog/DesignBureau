using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class InteractionDictionary
    {
        [Key]
        public int InteractionDictionaryId { get; set; }

        [Required]
        [StringLength(256)]
        public string InteractionTitle { get; set; }

        // Navigation property
        //[InverseProperty("InteractionDictionary")]
        //public ICollection<Interaction> Interactions { get; set; }
    }
}
