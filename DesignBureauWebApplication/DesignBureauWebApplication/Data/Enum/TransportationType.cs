using System.ComponentModel.DataAnnotations;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum TransportationType
    {
        [Display (Name="По воздуху🛫")]
        Air, // воздушная перевозка
        [Display (Name= "По дороге🚛")]
        Road, // автомобильная или дорожная перевозка
        [Display (Name= "По реальсам🚂")]
        Rail, // железнодорожная или поездная перевозка
        [Display (Name= "По воде🚢")]
        Water, // водная или судовая перевозка
        [Display(Name= "Комбинированная🔄")]
        Multimodal // комбинированная или мультимодальная перевозка
    }
}
