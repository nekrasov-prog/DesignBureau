using DesignBureauWebApplication.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        [StringLength(512)]
        public string? City { get; set; }
        [StringLength(512)]
        public string? Street { get; set; }
        [StringLength(8)]
        public string? HouseNumber { get; set; }

        public LocationType? LocationType { get; set; }

        //[InverseProperty("Origin")]
        //public virtual ICollection<Transportation> Origins { get; set; }
        //[InverseProperty("Destination")]
        //public virtual ICollection<Transportation> Destinations { get; set; }

        //[InverseProperty("Location")]
        //public virtual ICollection<Department> Departments { get; set; }
    }

}
