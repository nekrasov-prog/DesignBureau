using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum EquipmentStatus
    {
        [Display(Name = "🆕Добавлен")]
        New,
        [Display(Name = "🟢Работает")]
        Working,
        [Display(Name = "🔴Сломан")]
        OutOfService,
        [Display(Name = "🟡В ремонте")]
        UnderRepair,
        [Display(Name = "🟣Забронирован")]
        Reserved
    }
}
