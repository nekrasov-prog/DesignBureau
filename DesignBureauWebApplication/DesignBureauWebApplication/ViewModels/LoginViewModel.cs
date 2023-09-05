using System.ComponentModel.DataAnnotations;

namespace DesignBureauWebApplication.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}