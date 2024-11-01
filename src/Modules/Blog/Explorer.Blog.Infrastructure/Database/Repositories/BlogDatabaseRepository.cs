using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public Result<Post> Create(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return Result.Ok(post);
        }

        public Result<Post> GetById(long postId)
        {
            var post = _context.Posts
                .FirstOrDefault(p => p.Id == postId);

            return post != null ? Result.Ok(post) : Result.Fail("Post not found");
        }

        public Result<Post> Update(Post post)
        {
            if (!_context.Posts.Any(p => p.Id == post.Id))
                return Result.Fail("Post not found");

            _context.Posts.Update(post);
            _context.SaveChanges();
            return Result.Ok(post);
        }

        public Result Delete(long postId)
        {
            var post = _context.Posts.Find(postId);
            if (post == null)
                return Result.Fail("Post not found");

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result<IEnumerable<Post>> GetAll(int pageNumber, int pageSize)
        {
            var posts = _context.Posts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Result.Ok(posts.AsEnumerable());
        }
    }
}
