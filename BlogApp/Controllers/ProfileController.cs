using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
