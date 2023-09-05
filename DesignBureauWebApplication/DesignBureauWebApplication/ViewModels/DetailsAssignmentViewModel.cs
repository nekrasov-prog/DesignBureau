using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class DetailsAssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public int AssignmentDictionaryId { get; set; }
        public  AssignmentDictionary AssignmentDictionary { get; set; }
        public int WorkId { get; set; }
        public Work? Work { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AssignmentHistory> AssignmentHistories { get; set; }
        public List<Consumption> Consumptions { get; set; }
        public List<Execution> Executions { get; set; }
        public AssignmentStatus LastAssignmentStatus { get; set; }
    }
}
