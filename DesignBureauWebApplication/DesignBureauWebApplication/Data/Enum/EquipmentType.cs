using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum EquipmentType
    {
        [Display(Name = "🤖 Робот")]
        Robot,
        [Display(Name = "🖥️ Компьютер")]
        Computer,
        [Display(Name = "🔬 Инструмент")]
        Instrument,
        [Display(Name = "⚙️ Машина")]
        Machine,
        [Display(Name = "🎛️ Устройство")]
        Device
    }
}
