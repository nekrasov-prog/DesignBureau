using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsProjectViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanOverDate { get; set; }
        public List<Work> Works { get; set; }
        public List<ProjectHistory> ProjectHistories { get; set; }
        public ProjectStatus LastProjectStatus { get; set; }
        public int? Progress { get; set; }
    }
}
