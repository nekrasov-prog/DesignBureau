using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [StringLength(512)]
        public string JobTitle { get; set; }

        //[Required]
        //[ForeignKey("Department")]
        //public int DepartmentId { get; set; }

        //public Department Department { get; set; }

        //[InverseProperty("Position")]
        //public ICollection<Employee> Employees { get; set; }
    }

}
