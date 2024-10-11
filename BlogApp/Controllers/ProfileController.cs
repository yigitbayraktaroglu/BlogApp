using BlogApp.Business.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

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
                    Title = blog.Title,
                    Content = blog.Content,
                    UpdatedDate = DateTime.Now
                });
            }
            profileViewModel.Blogs = blogViewList;

            return View(profileViewModel);
        }
    }
}
