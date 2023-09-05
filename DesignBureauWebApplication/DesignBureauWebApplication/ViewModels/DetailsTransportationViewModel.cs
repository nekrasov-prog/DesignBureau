using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsTransportationViewModel
    {
        public int TransportationId { get; set; }
        public DateTime? PlanDepartureDateTime { get; set; }
        public DateTime PlanArrivalDateTime { get; set; }
        public int? OriginId { get; set; }
        public Location? Origin { get; set; }
        public int DestinationId { get; set; }
        public Location Destination { get; set; }
        public List<TransportationHistory> TransportationHistories { get; set; }
        public List<InventoryTransportation> InventoryTransportations { get; set; }
        public TransportationStatus LastTransportationStatus { get; set; }
        public Order? Order { get; set; }
    }
}
