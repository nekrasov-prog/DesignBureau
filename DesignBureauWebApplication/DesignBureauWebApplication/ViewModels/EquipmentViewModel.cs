using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class EquipmentViewModel
    {
        public int EquipmentId { get; set; }
        public int EquipmentDictionaryId { get; set; }
        public SelectList? EquipmentDictionaryList { get; set; }
        public int? LocationId { get; set; } 
        public SelectList? LocationList { get; set; }
        public EquipmentStatus? EquipmentStatus { get; set;}
        //public List<SelectListItem>? EquipmentStatus { get; set;}
    }
}
