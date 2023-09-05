using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignBureauWebApplication.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(256)]
        public string ELastName { get; set; }

        [Required]
        [StringLength(256)]
        public string EFirstName { get; set; }

        [StringLength(256)]
        public string? EPatronymic { get; set; }

        public DateTime? BirthDate { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [StringLength(128)]
        public string PhoneNumber { get; set; }

        [Required]
        [ForeignKey("Position")]
        public int PositionId { get; set; }

        public virtual Position Position { get; set; }
        public string? UserRole { get; set; }
        public string? Password { get; set; }

        //[InverseProperty("Employee")]
        //public virtual ICollection<Interaction> Interactions { get; set; }
    }
}
