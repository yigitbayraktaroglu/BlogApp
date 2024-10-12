using BlogApp.Entity.Entities;

namespace BlogApp.Areas.Admin.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public Blog Blogs { get; set; }

        public int BlogId { get; set; }


        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }

    }
}
