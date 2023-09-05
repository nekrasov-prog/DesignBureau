using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum QualityCheckingGrade
    {
        [Display(Name = "Одобрено👍")]
        Approved,
        [Display(Name = "Отклонено👎")]
        Declined
    }
}
