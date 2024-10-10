using BlogApp.Business.Abstract;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entity.Entities;

namespace BlogApp.Business.Concrete
{
    public class LogManager : ILogService
    {
        private readonly ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public void Delete(Log t)
        {
            _logDal.Delete(t);
        }

        public Log GetById(int id)
        {
            return _logDal.GetById(id);
        }

        public List<Log> GetListAll()
        {
            return _logDal.GetListAll();
        }

        public void Insert(Log t)
        {
            _logDal.Insert(t);
        }

        public void Update(Log t)
        {
            _logDal.Update(t);
        }
    }
}
