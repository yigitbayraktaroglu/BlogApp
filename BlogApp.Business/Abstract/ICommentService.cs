using BlogApp.Entity.Entities;

namespace BlogApp.Business.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {

        public List<Comment> GetListByBlogId(string BlogId);
    }
}
