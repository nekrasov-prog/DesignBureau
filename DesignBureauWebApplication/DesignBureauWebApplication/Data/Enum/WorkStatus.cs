using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum WorkStatus
    {
        [Display(Name = "Подготовка🚧")]
        Preparation,
        [Display(Name = "В процессе⏳")]
        InProgress,
        [Display(Name = "Завершена🏁")]
        Completed,
        [Display(Name = "Отменена❌")]
        Cancelled,
        [Display(Name = "Приостановлена⏸️")]
        Paused
    }
}
