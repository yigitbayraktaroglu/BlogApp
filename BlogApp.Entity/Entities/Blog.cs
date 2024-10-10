namespace BlogApp.Entity.Entities
{
    public class Blog
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

        public int IdCategory { get; set; }


        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


    }
}
