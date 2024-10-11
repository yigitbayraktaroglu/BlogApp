using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        private readonly BlogAppContext _context;
        public EfAppUserDal(BlogAppContext context) : base(context)
        {
            _context = context;
        }
        public AppUser GetByUsername(string username)
        {
            return _context.Set<AppUser>().Where(entity => entity.UserName == username).FirstOrDefault();
        }
    }
}
