using BlogApp.Areas.Admin.Models;
using BlogApp.Business.Abstract;
using BlogApp.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
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


        public IActionResult Index()
        {
            var blogs = _blogService.GetListAll()
    .OrderByDescending(b => b.CreatedDate)
    .ToList();

            var blogViewList = new List<BlogViewModel>();

            foreach (var blog in blogs)
            {
                blogViewList.Add(new BlogViewModel
                {
                    Id = blog.Id,
                    AppUser = _appUserService.GetById(blog.AppUserId),
                    Categories = _categoryService.GetById(blog.CategoryId),
                    Title = blog.Title,
                    Content = blog.Content,
                    Highlight = blog.Highlight,
                    CreatedDate = blog.CreatedDate,
                    UpdatedDate = blog.UpdatedDate,
                    IsDeleted = blog.IsDeleted,
                    IsDraft = blog.IsDraft,
                    AppUserId = blog.AppUserId,
                    CategoryId = blog.CategoryId,
                });
            }

            return View(blogViewList);

        }

        [HttpGet]
        [Route("Admin/Blog/Edit")]
        public IActionResult Edit(string id)
        {

            var blog = _blogService.GetById(int.Parse(id));

            if (blog == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var categories = _categoryService.GetListAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var model = new BlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Highlight = blog.Highlight,
                CreatedDate = blog.CreatedDate,
                UpdatedDate = blog.UpdatedDate,
                IsDeleted = blog.IsDeleted,
                IsDraft = blog.IsDraft,
                AppUserId = blog.AppUserId,
                CategoryId = blog.CategoryId,
            };



            return View(model);
        }

        [HttpPost]
        [Route("Admin/Blog/Edit")]
        public IActionResult Edit(int id, BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blog = _blogService.GetById(id);


                blog.Title = model.Title;
                blog.Content = model.Content;
                blog.CategoryId = model.CategoryId;
                blog.UpdatedDate = DateTime.Now;
                blog.IsDraft = model.IsDraft;
                blog.Highlight = model.Highlight;

                _blogService.Update(blog);

                return RedirectToAction("Blog", "Admin");
            }

            return View(model);
        }

        [HttpPost]
        [Route("Admin/Blog/Delete")]

        public IActionResult DeleteBlog(int id)
        {
            var blog = _blogService.GetById(id);


            _blogService.Delete(blog);

            return Ok();
        }
    }
}
