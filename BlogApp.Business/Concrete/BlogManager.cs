using BlogApp.Business.Abstract;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entity.Entities;

namespace BlogApp.Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private readonly IBlogDal _blogdal;

        public BlogManager(IBlogDal blogdal)
        {
            _blogdal = blogdal;
        }

        public void Delete(Blog t)
        {
            _blogdal.Delete(t);
        }

        public Blog GetById(int id)
        {
            return _blogdal.GetById(id);
        }

        public List<Blog> GetListAll()
        {
            return _blogdal.GetListAll();
        }

        public void Insert(Blog t)
        {
            _blogdal.Insert(t);
        }

        public void Update(Blog t)
        {
            _blogdal.Update(t);
        }
    }
}
