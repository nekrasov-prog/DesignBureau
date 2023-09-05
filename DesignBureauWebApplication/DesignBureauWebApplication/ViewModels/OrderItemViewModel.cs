using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int InventoryId { get; set; }
        public int SelectedEquipmentDictionaryId { get; set; }
        public SelectList? EquipmentDictionaryList { get; set; }
        public int SelectedMaterialDictionaryId { get; set; }
        public SelectList? MaterialDictionaryList { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
    }
}
