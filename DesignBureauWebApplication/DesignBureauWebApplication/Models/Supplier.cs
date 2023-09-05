using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        //[Required]
        [StringLength(512)]
        public string SName { get; set; }

        //[Required]
        [Phone]
        [StringLength(128)]
        public string PhoneNumber { get; set; }

        //[Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        //[InverseProperty("Supplier")]
        //public ICollection<Order> Orders { get; set; }
    }

}
