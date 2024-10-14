using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly BlogAppContext _context;
        public EfCommentDal(BlogAppContext context) : base(context)
        {
            _context = context;
        }


        public List<Comment> GetListByBlogId(int blogId)
        {
            return _context.Comments
                           .Where(comment => comment.BlogId == blogId)
                           .ToList();
        }

    }
}
