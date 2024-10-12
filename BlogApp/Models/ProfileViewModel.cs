namespace BlogApp.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Username { get; set; }

        public List<BlogViewModel> Blogs { get; set; }

    }
}
