using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignBureauWebApplication.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Введите пароль еще раз")]
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public int EmployeeId { get; set; }
        public string ELastName { get; set; }
        public string EFirstName { get; set; }
        public string? EPatronymic { get; set; }
        public DateTime? BirthDate { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public int PositionId { get; set; }
        public SelectList? PositionList { get; set; }
        public string UserRole { get; set; }
        public SelectList? UserRoleList { get; set; }
    }
}