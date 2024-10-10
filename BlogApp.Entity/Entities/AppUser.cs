using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        // Collection to hold the blogs written by the user
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

        // Collection to hold the comments made by the user
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
