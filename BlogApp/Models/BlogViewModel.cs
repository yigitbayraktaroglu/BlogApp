namespace BlogApp.Models
{
    public class BlogViewModel
    {
        public string AppUserId { get; set; }
        public string AuthorUsername { get; set; }

        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }
        public int Highlight { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDraft { get; set; }






    }
}
