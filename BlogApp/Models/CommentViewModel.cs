namespace BlogApp.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string AuthorUsername { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BlogId { get; set; }
    }
}
