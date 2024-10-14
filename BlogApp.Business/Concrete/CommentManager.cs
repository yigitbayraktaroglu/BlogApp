using BlogApp.Business.Abstract;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entity.Entities;

namespace BlogApp.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        private readonly IAppUserService _appUserService;
        public CommentManager(ICommentDal commentDal, IAppUserService appUserService)
        {
            _commentDal = commentDal;
            _appUserService = appUserService;
        }
        public void Delete(Comment t)
        {
            _commentDal.Delete(t);
        }


        public Comment GetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> GetListAll()
        {
            return _commentDal.GetListAll();
        }

        public List<Comment> GetListByBlogId(int BlogId)
        {
            var comments = _commentDal.GetListByBlogId(BlogId);
            foreach (var comment in comments)
            {
                comment.AppUser = _appUserService.GetById(comment.AppUserId);
            }

            return comments;
        }

        public void Insert(Comment t)
        {
            _commentDal.Insert(t);
        }

        public void Update(Comment t)
        {
            _commentDal.Update(t);
        }
    }
}
