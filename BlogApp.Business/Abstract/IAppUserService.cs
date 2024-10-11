using BlogApp.Entity.Entities;

namespace BlogApp.Business.Abstract
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        AppUser GetByUsername(string username);
    }
}
