using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace DesignBureauWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPositionRepository _positionRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IPositionRepository positionRepository, IEmployeeRepository employeeRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _positionRepository = positionRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //Пользователь найден, проверка пароля
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Пароль верный, вход
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Project");
                    }
                }
                //Пароль неверный
                TempData["Error"] = "Неправильные логин или пароль. Попробуйте снова";
                return View(loginViewModel);
            }
            //Пользователь не найден
            TempData["Error"] = "Неправильные логин или пароль. Попробуйте снова";
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var userRoles = typeof(UserRoles)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null).ToString())
                .ToList();
            var positions = await _positionRepository.GetAll();
            var registerVM = new RegisterViewModel
            {
                UserRoleList = new SelectList(userRoles),
                PositionList = new SelectList(positions, "PositionId", "JobTitle")
            };
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Адрес уже используется";
                return View(registerVM);
            }

            var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
            if (passwordValidator != null)
            {
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, null, registerVM.Password);
                if (!passwordValidationResult.Succeeded)
                {
                    foreach (var error in passwordValidationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, "Пароль должен содержать латинские буквы обоих регистров, хотя бы одно число и специальный символ");
                    }
                    var userRoles = typeof(UserRoles)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Select(f => f.GetValue(null).ToString())
                        .ToList();
                    var positions = await _positionRepository.GetAll();
                    registerVM = new RegisterViewModel
                    {
                        UserRoleList = new SelectList(userRoles),
                        PositionList = new SelectList(positions, "PositionId", "JobTitle")
                    };
                    return View(registerVM);
                }
            }
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                var employee = new Employee
                {
                    ELastName = registerVM.ELastName,
                    EFirstName = registerVM.EFirstName,
                    EPatronymic = registerVM.EPatronymic,
                    Email = registerVM.EmailAddress,
                    PhoneNumber = registerVM.PhoneNumber,
                    BirthDate = registerVM.BirthDate,
                    Password = registerVM.Password,
                    PositionId = registerVM.PositionId,
                    UserRole = registerVM.UserRole
                };
                _employeeRepository.Add(employee);
                newUser.EmployeeId = employee.EmployeeId;
                await _userManager.UpdateAsync(newUser);

                if (registerVM.UserRole == "admin")
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                }
                else if (registerVM.UserRole == "user")
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
                else if (registerVM.UserRole == "project_manager")
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.ProjectManager);
                }
            }

            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }

}
