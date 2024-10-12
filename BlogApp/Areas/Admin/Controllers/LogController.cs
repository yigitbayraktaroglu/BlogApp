using BlogApp.Areas.Admin.Models;
using BlogApp.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            var logs = _logService.GetListAll();
            var logViewList = new List<LogViewModel>();
            foreach (var log in logs)
            {
                logViewList.Add(new LogViewModel
                {
                    Id = log.Id,
                    LogName = log.LogName,
                    LogDescription = log.LogDescription,
                    LogDate = log.LogDate
                });

            }
            return View(logViewList);
        }
    }
}