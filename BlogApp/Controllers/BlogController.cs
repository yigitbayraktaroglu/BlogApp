using BlogApp.Business.Abstract;
using BlogApp.Entity.Entities;
using BlogApp.Models;
using BlogApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly ILoggerService _loggerService;

        public BlogController(IAppUserService appUserService, IBlogService blogService, ICategoryService categoryService, ICommentService commentService, ILoggerService loggerService)
        {
            _appUserService = appUserService;
            _blogService = blogService;
            _categoryService = categoryService;
            _commentService = commentService;
            _loggerService = loggerService;
        }

        [Route("Blog/Detail/{id}")]
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
                    AuthorUsername = c.AppUser.UserName,
                    Content = c.Content,
                    CreatedDate = c.CreatedDate
                }).ToList() // Yorumları CommentViewModel'e dönüştürüyoruz
            };

            // View'e BlogViewModel'i gönder
            return View(blogViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var Id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                // Yorumun veritabanına eklenmesi
                var comment = new Comment
                {
                    Content = commentViewModel.Content,
                    CreatedDate = DateTime.Now,
                    BlogId = commentViewModel.BlogId,
                    AppUserId = Id
                };

                _commentService.Insert(comment);

                _loggerService.Log("Add Comment", $"{commentViewModel.AuthorUsername} add comment. BlogID: {commentViewModel.BlogId}");
                // Blog detay sayfasına geri dönüyoruz
                return RedirectToAction("Index", new { id = commentViewModel.BlogId });
            }

            // Eğer formda hata varsa tekrar blog sayfasına dön
            return View(commentViewModel);
        }

        [HttpGet]
        [Route("Blog/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            // Veritabanından blogu alıyoruz.
            var blog = _blogService.GetById(id);

            if (blog == null || blog.AppUserId.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Error", "Home"); // Blog bulunamazsa veya kullanıcı yetkili değilse hata sayfasına yönlendir.
            }
            var categories = _categoryService.GetListAll(); // Kategorileri alın
            ViewBag.Categories = new SelectList(categories, "Id", "Name"); // SelectList oluşturun
            // Blog'u ViewModel'e dönüştürüp edit sayfasına gönderiyoruz.
            var model = new BlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                CategoryId = blog.CategoryId.ToString(),
                IsDraft = blog.IsDraft,
                // Diğer alanlar da burada eklenebilir.
            };



            return View(model);
        }

        [HttpPost]
        [Route("Blog/Edit/{id}")]
        public IActionResult Edit(int id, BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blog = _blogService.GetById(id);

                if (blog == null || blog.AppUserId.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return RedirectToAction("Error", "Home"); // Blog bulunamazsa veya kullanıcı yetkili değilse hata sayfasına yönlendir.
                }

                // Blog bilgilerini güncelliyoruz.
                blog.Title = model.Title;
                blog.Content = model.Content;
                blog.CategoryId = Int32.Parse(model.CategoryId);
                blog.UpdatedDate = DateTime.Now;
                blog.IsDraft = model.IsDraft;

                _blogService.Update(blog);
                _loggerService.Log("Edit Blog", $"{User.Identity.Name} edit blog. BlogID: {id}");
                return RedirectToAction("Detail", new { id = blog.Id }); // Düzenlemeden sonra blog detay sayfasına yönlendirme.
            }

            return View(model); // Model geçerli değilse tekrar düzenleme sayfasını göster.
        }

        [HttpPost]
        [Route("Blog/Delete/{id}")]

        public IActionResult DeleteBlog(int id)
        {
            var blog = _blogService.GetById(id);

            if (blog == null || blog.AppUserId.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Error", "Home");
            }

            _blogService.Delete(blog);
            _loggerService.Log("Delete Blog", $"{User.Identity.Name} delete blog. BlogID: {id}");
            return Ok(); // Silme işleminden sonra blog listesini gösterecek şekilde yönlendirin.
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoryService.GetListAll(); // Kategorileri alın
            ViewBag.Categories = new SelectList(categories, "Id", "Name"); // SelectList oluşturun
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Burada blogu veritabanına kaydedin
                var newBlog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    AppUserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    IsDeleted = false,
                    IsDraft = model.IsDraft,
                    CategoryId = Int32.Parse(model.CategoryId),

                };

                // Veritabanına kaydetme işlemi
                _blogService.Insert(newBlog);
                _loggerService.Log("Create Blog", $"{User.Identity.Name} create blog. BlogTitle: {model.Title}");
                return RedirectPermanent("/Profile/" + _appUserService.GetById(newBlog.AppUserId).UserName);
            }

            return View(model); // Eğer ModelState geçerli değilse, aynı sayfayı yeniden göster
        }



    }
}
