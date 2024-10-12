using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class BlogViewModel
    {
        public string AppUserId { get; set; }
        public string AuthorUsername { get; set; }

        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(45, ErrorMessage = "Title Max Length is 45")]
        public string Title { get; set; }
        public int Highlight { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDraft { get; set; }








    }
}
