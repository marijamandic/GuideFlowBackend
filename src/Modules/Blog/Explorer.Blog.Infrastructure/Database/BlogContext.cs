﻿using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
    }
}