namespace BlogApp.Entity.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();


    }
}
