using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum AssignmentStatus
    {
        [Display(Name = "Создана📝")]
        Created,
        [Display(Name = "Назначена📝")]
        Preparation,
        [Display(Name = "В процессе🕒")]
        InProgress,
        [Display(Name = "Завершена✅")]
        Completed,
        [Display(Name = "Отменена❌")]
        Cancelled,
        [Display(Name = "Перезапущена🔁")]
        Rebooted,
        [Display(Name = "Приостановлена⏸️")]
        Paused
    }
}
