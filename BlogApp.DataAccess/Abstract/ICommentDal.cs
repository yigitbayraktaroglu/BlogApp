using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        public List<Comment> GetListByBlogId(string BlogId);
    }
}
