using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using System.Linq.Expressions;

namespace BlogApp.DataAccess.Concrete
{
    public class GenericRepository<T> : IGenericDal<T> where T : class, new()
    {
        private readonly BlogAppContext _context;

        // Inject BlogAppContext via constructor
        public GenericRepository(BlogAppContext context)
        {
            _context = context;
        }

        public void Delete(T t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).ToList();
        }

        public void Insert(T t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _context.Update(t);
            _context.SaveChanges();
        }
    }
}
