using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class WorkDictionary
    {
        [Key]
        public int WorkDictionaryId { get; set; }

        [Required]
        [StringLength(256)]
        public string WorkTitle { get; set; }

        //[InverseProperty("WorkDictionary")]
        //public ICollection<Work> Works { get; set; }
    }
}
