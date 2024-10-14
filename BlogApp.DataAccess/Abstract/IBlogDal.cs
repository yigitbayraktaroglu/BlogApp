using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {

        public List<Blog> GetListByAppUserId(int appUserId);
    }
}
