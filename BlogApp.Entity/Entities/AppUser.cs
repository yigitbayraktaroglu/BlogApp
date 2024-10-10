using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }


        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();


        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
