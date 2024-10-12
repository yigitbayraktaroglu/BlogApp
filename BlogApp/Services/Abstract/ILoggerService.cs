namespace BlogApp.Services.Abstract
{
    public interface ILoggerService
    {
        Task Log(string name, string description);
    }
}
