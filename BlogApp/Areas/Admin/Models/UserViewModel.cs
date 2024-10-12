namespace BlogApp.Areas.Admin.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }

    }
}
