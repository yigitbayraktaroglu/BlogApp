using BlogApp.Business.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAppUserService _appUserService;

        public ProfileController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        // Profil sayfası, username'e göre açılır
        public IActionResult Index(string username)
        {
            // Burada username ile profile bilgilerini bulup modeli dönebilirsiniz
            var profile = _appUserService.GetByUsername(username);
            if (profile == null)
            {
                return NotFound(); // Profil bulunamazsa 404 döndür
            }

            var profileViewModel = new ProfileViewModel
            {
                Id = profile.Id.ToString(),
                Name = profile.Name,
                Username = profile.UserName
            };

            return View(profileViewModel);
        }
    }
}
