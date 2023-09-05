using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.ViewModels
{
    public class ConsumptionViewModel
    {
        public int ConsumptionId { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public IEnumerable<Material>? Materials { get; set; }
        public Dictionary<int, bool>? MaterialSelection { get; set; }
        public Dictionary<int, int>? Quantities { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
