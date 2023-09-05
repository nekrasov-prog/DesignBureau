using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum TransportationStatus
    {
        [Display(Name = "Создан📝")]
        Created, // перевозка создана в системе
        [Display(Name = "Запланирован🗓️")]
        Planned, // перевозка запланирована и назначена
        [Display(Name = "В пути🚛")]
        InTransit, // перевозка находится в пути
        [Display(Name = "Доставлен✅")]
        Delivered, // перевозка доставлена по адресу
        [Display(Name = "Отменен❌")]
        Cancelled // перевозка отменена
    }
}
