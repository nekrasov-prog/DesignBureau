using DesignBureauWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class TransportationHistory
    {
        [Key]
        public int TransportationHistoryId { get; set; }

        [Required]
        [ForeignKey("Transportation")]
        public int TransportationId { get; set; }
        public virtual Transportation Transportation { get; set; }

        [Required]
        public TransportationStatus TransportationStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
