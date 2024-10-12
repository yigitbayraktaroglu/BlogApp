using BlogApp.Areas.Admin.Models;
using BlogApp.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IAppUserService _appUserService;
        private readonly IBlogService _blogService;

        public CommentController(ICommentService commentService, IAppUserService appUserService, IBlogService blogService)
        {
            _commentService = commentService;
            _appUserService = appUserService;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var comments = _commentService.GetListAll().OrderByDescending(c => c.CreatedDate)
    .ToList(); ;
            var commentViewList = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                commentViewList.Add(new CommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedDate = comment.CreatedDate,
                    UpdatedDate = comment.UpdatedDate,
                    AppUser = _appUserService.GetById(comment.AppUserId),
                    Blogs = _blogService.GetById(comment.BlogId),
                });
            }


            return View(commentViewList);
        }
    }
}
