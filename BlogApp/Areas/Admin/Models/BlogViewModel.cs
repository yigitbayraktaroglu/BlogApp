using BlogApp.Entity.Entities;

namespace BlogApp.Areas.Admin.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public int Highlight { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDraft { get; set; }

        public Category Categories { get; set; }

        public int CategoryId { get; set; }

        public AppUser AppUser { get; set; }

        public int AppUserId { get; set; }



    }
}
