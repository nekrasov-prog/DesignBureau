using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsWorkViewModel
    {
        public int WorkId { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanOverDate { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public WorkDictionary WorkDictionary { get; set; }
        public int WorkDictionaryId { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<WorkHistory> WorkHistories { get; set; }
        public WorkStatus LastWorkStatus { get; set; }
        public int? Progress { get; set; }
    }
}
