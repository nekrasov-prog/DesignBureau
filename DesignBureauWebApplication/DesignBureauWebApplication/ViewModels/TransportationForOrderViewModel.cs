using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class TransportationForOrderViewModel
    {
        public int TransportationId { get; set; }
        public Transportation? Transportation { get; set; }
        public DateTime PlanArrivalDateTime { get; set; }
        public int DestinationId { get; set; }
        public SelectList? DestinationList { get; set; }
        public int InventoryTransportationId { get; set; }
        public IEnumerable<Inventory>? Inventories { get; set; }
        public Dictionary<int, bool>? InventorySelection { get; set; }
        public Dictionary<int, int>? Quantities { get; set; }
        public int OrderId { get; set; }
    }
}
