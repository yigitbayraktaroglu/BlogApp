using BlogApp.Entity.Entities;

namespace BlogApp.Business.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        public List<Blog> GetListByAppUserId(string appUserId);
    }
}
