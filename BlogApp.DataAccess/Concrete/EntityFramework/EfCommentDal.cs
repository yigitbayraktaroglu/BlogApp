using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public EfCommentDal(BlogAppContext context) : base(context)
        {
        }
    }
}
