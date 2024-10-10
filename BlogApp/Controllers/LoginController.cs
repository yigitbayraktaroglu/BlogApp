using BlogApp.Entity.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel p)
        {
            AppUser appUser = new AppUser()
            {

                Name = p.Name,
                Surname = p.Surname,
                Email = p.Mail,
                UserName = p.Username

            };
            if (p.Password == p.PasswordConfirm)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel p)
        {
            if (ModelState.IsValid)
            {
                var signinResult = await _signInManager.PasswordSignInAsync(
                 p.UserName, p.Password, false, false);
                if (signinResult.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(p.UserName);
                    if (signinResult.IsNotAllowed)
                    {
                        if (!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "Email not confirmed");

                        }
                    }
                    else
                    {

                        if (user == null)
                        {
                            ModelState.AddModelError("", "Username not found");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Password is wrong");
                        }
                    }



                }

            }
            return View();
        }
    }
}

