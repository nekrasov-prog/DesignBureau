using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace DesignBureauWebApplication.Models
{
    public class EquipmentDictionary
    {
        [Key]
        public int EquipmentDictionaryId { get; set; }

        [Required]
        [StringLength(512)]
        public string EquipmentName { get; set; }

        [Required]
        public EquipmentType EquipmentType { get; set; }
    }
}
