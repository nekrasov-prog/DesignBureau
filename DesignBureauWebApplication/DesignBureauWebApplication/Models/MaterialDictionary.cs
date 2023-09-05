using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class MaterialDictionary
    {
        [Key]
        public int MaterialDictionaryId { get; set; }

        [Required]
        [StringLength(512)]
        public string MaterialName { get; set; }

        [Required]
        public MaterialType MaterialType { get; set; }
    }
}
