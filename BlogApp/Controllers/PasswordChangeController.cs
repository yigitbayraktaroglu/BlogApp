using BlogApp.Entity.Entities;
using BlogApp.Models;
using BlogApp.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Controllers
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILoggerService _loggerService;
        public PasswordChangeController(UserManager<AppUser> userManager, IEmailService emailService, ILoggerService loggerService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Mail);
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(forgotPasswordViewModel.Mail, "Reset your password",
              $"Please reset your password by clicking this link: {passwordResetTokenLink}");


            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userid, string token)
        {
            TempData["userid"] = userid;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userid"];
            var token = TempData["token"];
            if (userid == null || token == null)
            {

            }
            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                await _loggerService.Log("Reset Password", $"{user.UserName} changed password.");
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
    }
}

