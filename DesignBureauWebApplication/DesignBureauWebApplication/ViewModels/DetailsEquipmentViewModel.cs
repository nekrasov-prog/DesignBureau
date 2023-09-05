using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsEquipmentViewModel
    {
        public int EquipmentId { get; set; }
        public int EquipmentDictionaryId { get; set; }
        public EquipmentDictionary EquipmentDictionary { get; set; }
        public EquipmentStatus ActualEquipmentStatus { get; set; }
        public Inventory? Inventory { get; set; }
        public List<EquipmentHistory> EquipmentHistories { get; set; }
    }
}