using BlogApp.Business.Abstract;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entity.Entities;

namespace BlogApp.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
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
