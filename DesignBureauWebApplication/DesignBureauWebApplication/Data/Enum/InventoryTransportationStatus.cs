using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum InventoryTransportationStatus
    {
        [Display(Name = "📦 Получен")]
        Received,
        [Display(Name = "🚫 Потерян")]
        Lost
        /*,
        [Display(Name = "🔙 Возвращен")]
        Returned*/
    }
}
