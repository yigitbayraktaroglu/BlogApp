using BlogApp.Business.Abstract;
using BlogApp.Entity.Entities;
using BlogApp.Services.Abstract;

namespace BlogApp.Services.Concrete
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogService _logService;

        public LoggerService(ILogService logService)
        {
            _logService = logService;
        }

        public Task Log(string name, string description)
        {
            var log = new Log();

            log.LogDate = DateTime.Now;
            log.LogDescription = description;
            log.LogName = name;

            _logService.Insert(log);

            return Task.CompletedTask;
        }
    }
}
