using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        [Required]
        [ForeignKey("MaterialDictionary")]
        public int MaterialDictionaryId { get; set; }
        public virtual MaterialDictionary MaterialDictionary { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Inventory Inventory { get; set; }
        public List<Consumption> Consumptions { get; set; }
    }
}
