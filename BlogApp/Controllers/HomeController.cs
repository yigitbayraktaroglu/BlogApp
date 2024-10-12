using BlogApp.Business.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    private readonly IAppUserService _appUserService;


    public HomeController(IBlogService blogService, ICategoryService categoryService, IAppUserService appUserService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
        _appUserService = appUserService;
    }


    public IActionResult Index(int CategoryId)
    {

        var blogList = _blogService.GetListAll()
        .Where(b => !b.IsDeleted) // Exclude deleted blogs
        .OrderByDescending(b => b.Highlight) // Sort by highlight value
        .ToList();

        if (CategoryId != 0)
        {
            blogList = blogList.Where(b => b.CategoryId == CategoryId).ToList();
        }
        var blogViewList = new List<BlogViewModel>();

        var categoryList = _categoryService.GetListAll();
        var categoryViewList = new List<CategoryViewModel>();
        foreach (var category in categoryList)
        {
            categoryViewList.Add(new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
            });
        }


        foreach (var blog in blogList)
        {
            blogViewList.Add(new BlogViewModel
            {
                Id = blog.Id,
                CategoryName = _categoryService.GetById(blog.CategoryId).Name,
                AuthorUsername = _appUserService.GetById(blog.AppUserId).UserName,
                Title = blog.Title,
                Content = blog.Content,
                CreatedDate = blog.CreatedDate,
                IsDraft = blog.IsDraft,
            });
        }
        ViewBag.Categories = categoryViewList;
        ViewBag.SelectedCategory = CategoryId == 0 ? null : _categoryService.GetById(CategoryId).Name;

        return View(blogViewList);
    }

}
