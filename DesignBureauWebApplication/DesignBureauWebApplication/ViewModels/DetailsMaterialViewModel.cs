using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsMaterialViewModel
    {
        public int MaterialId { get; set; }
        public int MaterialDictionaryId { get; set; }
        public MaterialDictionary MaterialDictionary { get; set; }
        public int Quantity { get; set; }
        public Inventory? Inventory { get; set; }
        public List<Consumption> Consumptions { get; set; }
        public string? WhereIs { get; set; }
        public int? OrderId { get; set; }
    }
}
