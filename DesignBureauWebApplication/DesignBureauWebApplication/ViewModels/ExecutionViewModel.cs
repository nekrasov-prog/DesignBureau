using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesignBureauWebApplication.ViewModels
{
    public class ExecutionViewModel
    {
        public int ExecutionId { get; set; }
        public int AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }
        public int EmployeeId { get; set; }
        public SelectList? EmployeeList { get; set; }
    }
}
