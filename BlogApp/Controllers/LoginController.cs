using BlogApp.Entity.Entities;
using BlogApp.Models;
using BlogApp.Services.Abstract;
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
        private readonly IEmailService _emailService;
        private readonly ILoggerService _loggerService;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, ILoggerService loggerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _loggerService = loggerService;
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
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Login", new { userId = appUser.Id, token = token }, Request.Scheme);

                    // E-posta Gönderme Servisini Kullan
                    await _emailService.SendEmailAsync(appUser.Email, "Confirm your email",
                        $"Please confirm your account by clicking this link: {confirmationLink}");

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

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home"); // Hatalı istek durumunda anasayfaya yönlendir
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with ID = {userId} not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _loggerService.Log("Confirm Email", $"{User.Identity.Name} confirm email");
                return RedirectToAction("Index", "Home"); // Onay başarılı ise onay sayfasını göster
            }
            return View("Error"); // Hata sayfası
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Anasayfaya yönlendir
        }
    }


}

