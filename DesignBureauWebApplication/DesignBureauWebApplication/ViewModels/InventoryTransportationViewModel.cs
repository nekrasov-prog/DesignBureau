using DesignBureauWebApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.ViewModels
{
    public class InventoryTransportationViewModel
    {
        public int InventoryTransportationId { get; set; }
        public int TransportationId { get; set; }
        public Transportation? Transportation { get; set; }
        public IEnumerable<Inventory>? Inventories { get; set; }
        public Dictionary<int, bool>? InventorySelection { get; set; }
        public Dictionary<int, int>? Quantities { get; set; }
    }
}
