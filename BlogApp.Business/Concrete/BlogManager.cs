using BlogApp.Business.Abstract;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entity.Entities;

namespace BlogApp.Business.Concrete
{

    public class BlogManager : IBlogService
    {
        private readonly IBlogDal _blogdal;
        private readonly IAppUserService _appUserService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;

        public BlogManager(IBlogDal blogdal, IAppUserService appUserService, ICategoryService categoryService, ICommentService commentService)
        {
            _blogdal = blogdal;
            _appUserService = appUserService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        public void Delete(Blog t)
        {
            _blogdal.Delete(t);
        }

        public Blog GetById(int id)
        {
            var blog = _blogdal.GetById(id);

            if (blog == null) return null;

            blog.AppUser = _appUserService.GetById(blog.AppUserId);
            blog.Categories = _categoryService.GetById(blog.CategoryId);
            blog.Comments = _commentService.GetListByBlogId(blog.Id.ToString());
            return blog;
        }

        public List<Blog> GetListAll()
        {
            return _blogdal.GetListAll();
        }

        public List<Blog> GetListByAppUserId(string appUserId)
        {
            return _blogdal.GetListByAppUserId(appUserId);
        }

        public void Insert(Blog t)
        {
            _blogdal.Insert(t);
        }

        public void Update(Blog t)
        {
            _blogdal.Update(t);
        }
    }
}
