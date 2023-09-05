using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.ViewModels
{
    public class AssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public int AssignmentDictionaryId { get; set; }
        public  SelectList? AssignmentDictionaryList { get; set; }
        public int WorkId { get; set; }
        public Work? Work { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? NewAssignmentDictionary { get; set; }
    }
}
