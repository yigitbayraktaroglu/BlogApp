using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(BlogAppContext context) : base(context)
        {
        }
    }
}
