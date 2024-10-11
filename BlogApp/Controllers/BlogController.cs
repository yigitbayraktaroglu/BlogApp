using BlogApp.Business.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public BlogController(IAppUserService appUserService, IBlogService blogService, ICategoryService categoryService)
        {
            _appUserService = appUserService;
            _blogService = blogService;
            _categoryService = categoryService;
        }

        [Route("Details/{id}")]
        public IActionResult Index(string id)
        {
            int x = int.Parse(id);
            // Blogu veritabanından bul
            var blog = _blogService.GetById(x);

            if (blog == null)
            {
                // Eğer blog bulunamazsa NotFound döner
                return View("Error");
            }

            // Blog'u BlogViewModel'e dönüştür
            var blogViewModel = new BlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                AuthorUsername = blog.AppUser.UserName,  // AppUser ile ilişki varsayıldı
                CategoryName = blog.Categories.Name,       // Category ile ilişki varsayıldı
                CreatedDate = blog.CreatedDate,
                UpdatedDate = blog.UpdatedDate,
                Comments = blog.Comments.Select(c => new CommentViewModel
                {
                    // Yorum yapan kullanıcının adı
                    Content = c.Content,
                    CreatedDate = c.CreatedDate
                }).ToList() // Yorumları CommentViewModel'e dönüştürüyoruz
            };

            // View'e BlogViewModel'i gönder
            return View(blogViewModel);
        }
    }
}
