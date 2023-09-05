using System.ComponentModel.DataAnnotations;

namespace DesignBureauWebApplication.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanOverDate { get; set; }
    }
}
