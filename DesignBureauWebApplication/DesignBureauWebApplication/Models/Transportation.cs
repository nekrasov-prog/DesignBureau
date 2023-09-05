using DesignBureauWebApplication.Data.Enum;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Transportation
    {
        [Key]
        public int TransportationId { get; set; }

        public DateTime? PlanDepartureDateTime { get; set; }

        [Required]
        public DateTime PlanArrivalDateTime { get; set; }

        [ForeignKey("Origin")]
        public int? OriginId { get; set; }
        public virtual Location? Origin { get; set; }

        [ForeignKey("Destination")]
        public int DestinationId { get; set; }
        public virtual Location Destination { get; set; }

        public List<TransportationHistory> TransportationHistories { get; set; }
        public List<InventoryTransportation> InventoryTransportations { get; set; }

        //[InverseProperty("Transportation")]
        //public ICollection<InventoryTransportation> InventoryTransportations { get; set; }
    }
}
