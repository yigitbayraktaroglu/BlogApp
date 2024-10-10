using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        public EfBlogDal(BlogAppContext context) : base(context)
        {
        }
    }
}
