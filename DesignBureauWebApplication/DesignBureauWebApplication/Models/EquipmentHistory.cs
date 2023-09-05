using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class EquipmentHistory
    {
        [Key]
        public int EquipmentHistoryId { get; set; }
        [Required]
        [ForeignKey("Equipment")]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public EquipmentStatus EquipmentStatus { get; set; }
        public DateTime StatusStart { get; set; }
        public DateTime? StatusEnd { get; set; }
    }
}