using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }

        [Required]
        [ForeignKey("EquipmentDictionary")]
        public int EquipmentDictionaryId { get; set; }
        public virtual EquipmentDictionary EquipmentDictionary { get; set; }
        public virtual Inventory Inventory { get; set; }
        public List<EquipmentHistory> EquipmentHistories { get; set; }
    }
}
