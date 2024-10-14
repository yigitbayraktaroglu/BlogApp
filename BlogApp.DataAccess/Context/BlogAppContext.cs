using BlogApp.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Context
{
    public class BlogAppContext : IdentityDbContext<AppUser, AppRole, int>
    {


        public BlogAppContext(DbContextOptions<BlogAppContext> options) : base(options)
        {
        }





        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Comment -> Blog relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blogs)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Comment -> AppUser relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Blog -> AppUser relationship
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.AppUser)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Blog -> Category relationship
            modelBuilder.Entity<Blog>()
             .HasOne(b => b.Categories)
             .WithMany(c => c.Blogs)
             .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }


}
