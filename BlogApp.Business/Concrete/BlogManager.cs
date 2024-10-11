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

        public BlogManager(IBlogDal blogdal, IAppUserService appUserService, ICategoryService categoryService)
        {
            _blogdal = blogdal;
            _appUserService = appUserService;
            _categoryService = categoryService;
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
            return _blogdal.GetById(id);
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
