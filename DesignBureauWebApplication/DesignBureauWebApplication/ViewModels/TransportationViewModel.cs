using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class TransportationViewModel
    {
        public int TransportationId { get; set; }
        public DateTime? PlanDepartureDateTime { get; set; }
        public DateTime PlanArrivalDateTime { get; set; }
        public int? OriginId { get; set; }
        public SelectList? OriginList { get; set; }
        public int DestinationId { get; set; }
        public SelectList? DestinationList { get; set; }
    }
}
