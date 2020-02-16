using FootballLeagueMvcProject.Models;
using FootballLeagueMvcProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByNameAsync(lvm.Login);

            if (user == null || !_signInManager.CheckPasswordSignInAsync(user, lvm.Password, false).Result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        /*Панель управления администратора. 
         
             Возможность создавать и удалять модераторов для ресурса.
             Роль модератора подразумевает возмжоность ТОЛЬКО добавлять профили игроков.
             Учетные записи модераторов генерируются методом CreateModerator.
             
             */
        public async Task<IActionResult> Dashboard()
        {

            var moders = await _userManager.GetUsersInRoleAsync("Moderator");

            IEnumerable<ModerViewModel> moderViewModels = moders.Select(m => new ModerViewModel
            {
                Login = m.UserName,
                UserId = m.Id,
                FullName = m.FullName

            });

            return View(moderViewModels);
        }


        //Метод создания учетной записи модератора.

        [HttpPost]
        public async Task<IActionResult> CreateModerator(string fullname)

        {

            string login = Path.GetRandomFileName().Replace(".", "");  //Генерация случайного набора символа в качестве логина, похожий механизм используется и для получения пароля.
            string password = Path.GetRandomFileName().Replace(".", "");

            var user = new ApplicationUser { UserName = login, FullName = fullname != null ? fullname : "Владелец неопределен" };

            await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, "Moderator");

            return Content($"Данные для входа в систему модератора: [login : {login}; passowrd: {password}]");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteModerator(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return RedirectToAction("Dashboard");
            }

            var result = await _userManager.DeleteAsync(user);

            return RedirectToAction("Dashboard");
        }

    }
}
