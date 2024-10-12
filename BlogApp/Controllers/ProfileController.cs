using BlogApp.Business.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public ProfileController(IAppUserService appUserService, IBlogService blogService, ICategoryService categoryService)
        {
            _appUserService = appUserService;
            _blogService = blogService;
            _categoryService = categoryService;
        }

        [Route("Profile/{username}")]
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
                Name = profile.Name + " " + profile.Surname,
                Username = profile.UserName
            };
            var blogViewList = new List<BlogViewModel>();

            var blogList = _blogService.GetListByAppUserId(profileViewModel.Id);
            foreach (var blog in blogList)
            {
                blogViewList.Add(new BlogViewModel
                {
                    Id = blog.Id,
                    CategoryName = _categoryService.GetById(blog.CategoryId).Name,
                    AppUserId = blog.AppUserId.ToString(),
                    AuthorUsername = _appUserService.GetById(blog.AppUserId).UserName,
                    Title = blog.Title,
                    Content = blog.Content,
                    CreatedDate = blog.CreatedDate,
                    IsDraft = blog.IsDraft,
                });
            }
            profileViewModel.Blogs = blogViewList;

            return View(profileViewModel);
        }

        [Route("Profile/Edit/{username}")]
        [HttpGet]
        public IActionResult Edit()
        {
            // Fetch the user's existing details from the database
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _appUserService.GetById(userId); // Implement GetUserById in your service

            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName
            };

            return View(model);
        }

        [Route("Profile/Edit/{username}")]
        [HttpPost]
        public IActionResult Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = _appUserService.GetById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                // Update user information
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.Username;

                _appUserService.Update(user); // Implement UpdateUser in your service

                return RedirectToAction("Index", new { username = user.UserName });
            }

            return View(model);
        }
    }
}

