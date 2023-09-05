using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }
        public int MaterialDictionaryId { get; set; }
        public SelectList? MaterialDictionaryList { get; set; }
        public int? LocationId { get; set; }
        public SelectList? LocationList { get; set; }
        public int Quantity { get; set; }
    }
}
