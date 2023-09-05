using System.ComponentModel.DataAnnotations;

namespace DesignBureauWebApplication.Models
{
    public class AssignmentDictionary
    {
        [Key]
        public int AssignmentDictionaryId { get; set; }

        [Required]
        [StringLength(256)]
        public string AssignmentTitle { get; set; }
    }
}
