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


    public IActionResult Index(string searchTerm, int CategoryId, string sortOrder)
    {

        var blogList = _blogService.GetListAll().AsQueryable();


        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string lowerSearchTerm = searchTerm.ToLower();
            blogList = blogList.Where(b => b.Title.ToLower().Contains(lowerSearchTerm) ||
                                           b.Content.ToLower().Contains(lowerSearchTerm));
        }

        switch (sortOrder)
        {
            case "popular":
                blogList = blogList.OrderByDescending(b => b.Highlight);
                break;
            case "title_desc":
                blogList = blogList.OrderByDescending(b => b.Title);
                break;
            case "date":
                blogList = blogList.OrderBy(b => b.CreatedDate);
                break;
            case "date_desc":
                blogList = blogList.OrderByDescending(b => b.CreatedDate);
                break;
            default:
                blogList = blogList.OrderBy(b => b.Title);
                break;
        }


        if (CategoryId != 0)
        {
            blogList = blogList.Where(b => b.CategoryId == CategoryId);
        }


        var finalBlogList = blogList.ToList();

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


        foreach (var blog in finalBlogList)
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

    public IActionResult Error() { return View(); }
    public IActionResult NotFound() { return View(); }

}
