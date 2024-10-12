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
                .WithMany(b => b.Comments)  // Assuming Blog has a collection of Comments
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);  // Prevent cascading deletes

            // Configure Comment -> AppUser relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)  // Assuming AppUser has a collection of Comments
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);  // Prevent cascading deletes

            // Configure Blog -> AppUser relationship
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.AppUser)
                .WithMany(u => u.Blogs)  // Assuming AppUser has a collection of Blogs
                .HasForeignKey(b => b.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes

            modelBuilder.Entity<Blog>()
             .HasOne(b => b.Categories) // Blog'un bir kategoriye ait olduğunu belirt
             .WithMany(c => c.Blogs) // Kategorinin birden fazla blogu olabileceğini belirt
             .HasForeignKey(b => b.CategoryId) // Foreign Key olarak CategoryId kullan
            .OnDelete(DeleteBehavior.Cascade); // Kategori silindiğinde ilgili blogları sil

            base.OnModelCreating(modelBuilder);
        }
    }


}
