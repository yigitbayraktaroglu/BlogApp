using BlogApp.Business.Abstract;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entity.Entities;

namespace BlogApp.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _userDal;

        public AppUserManager(IAppUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Delete(AppUser t)
        {
            _userDal.Delete(t);
        }

        public AppUser GetById(int id)
        {
            return _userDal.GetById(id);
        }

        public List<AppUser> GetListAll()
        {
            return _userDal.GetListAll();
        }

        public AppUser GetByUsername(string username)
        {
            return _userDal.GetByUsername(username);
        }

        public void Insert(AppUser t)
        {
            _userDal.Insert(t);
        }

        public void Update(AppUser t)
        {
            _userDal.Update(t);
        }
    }
}
