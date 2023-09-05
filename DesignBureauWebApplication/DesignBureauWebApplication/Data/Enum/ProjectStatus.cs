using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum ProjectStatus
    {
        [Display(Name = "Идея💡")]
        Idea,
        [Display(Name = "Запуск🚀")]
        Launch,
        [Display(Name = "Планирование📋")]
        Planning,
        [Display(Name = "Исполнение⚙️")]
        Execution,
        [Display(Name = "Завершен🎉")]
        Completed,
        [Display(Name = "Отменен🗑️")]
        Cancelled,
        [Display(Name = "Заморожен🧊")]
        Frozen,
        [Display(Name = "Разморожен🌡️")]
        Unfrozen
    }
}
