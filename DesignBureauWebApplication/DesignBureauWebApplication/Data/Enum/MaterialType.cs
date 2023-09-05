using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum MaterialType
    {
        [Display(Name = "🧲 Магнитные")] // обладают магнитными свойствами или реагируют на магнитное поле
        Magnetic,
        [Display(Name = "🛠️ Механические")] // для создания или соединения частей роботов или оборудования
        Mechanical,
        [Display(Name = "🔌 Электрические")] //  для передачи или хранения электрического тока или сигнала
        Electrical,
        [Display(Name = "💻 Программные")] // для создания или управления программным обеспечением роботов или оборудования
        Software,
        [Display(Name = "🧪 Химические")] // для создания или изменения химических свойств или реакций
        Chemical
    }
}
