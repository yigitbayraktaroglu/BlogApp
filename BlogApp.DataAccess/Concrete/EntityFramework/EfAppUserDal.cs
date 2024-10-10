using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        public EfAppUserDal(BlogAppContext context) : base(context)
        {

        }
    }
}
