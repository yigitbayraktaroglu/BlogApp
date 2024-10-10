using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfLogDal : GenericRepository<Log>, ILogDal
    {
        public EfLogDal(BlogAppContext context) : base(context)
        {
        }
    }
}
