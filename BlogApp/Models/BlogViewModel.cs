namespace BlogApp.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryName { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
