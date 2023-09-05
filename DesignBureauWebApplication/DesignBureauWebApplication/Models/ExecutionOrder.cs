using DesignBureauWebApplication.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class ExecutionOrder
    {
        [Key]
        public int ExecutionOrderId { get; set; }

        [Required]
        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }

        [ForeignKey("NextWork")]
        public int? NextWorkId { get; set; }
        public virtual Work? NextWork { get; set; }
    }
}
