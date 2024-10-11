﻿namespace BlogApp.Models
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Username { get; set; }

        public List<BlogViewModel> Blogs { get; set; }

    }
}
