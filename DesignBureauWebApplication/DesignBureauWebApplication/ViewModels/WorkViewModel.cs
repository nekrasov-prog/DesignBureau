using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class WorkViewModel
    {
        public int WorkId { get; set; }

        public DateTime PlanStartDate { get; set; }

        public DateTime PlanOverDate { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public SelectList? WorkDictionaryList { get; set; }
        public int WorkDictionaryId { get; set; }
        public string? NewWorkDictionary { get; set; }
    }
}
