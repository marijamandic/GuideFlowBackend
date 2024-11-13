using Explorer.Blog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }

    public DbSet<BlogRating> Ratings { get; set; }
    public DbSet<Post> Posts { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Ratings)
            .WithOne()
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

       
    }


}