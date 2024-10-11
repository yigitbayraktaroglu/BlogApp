using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Abstract
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        AppUser GetByUsername(string username);
    }
}
