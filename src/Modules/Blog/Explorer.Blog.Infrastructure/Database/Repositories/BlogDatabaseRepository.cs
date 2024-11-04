using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var posts = _context.Posts.AsEnumerable(); // Fetch all posts without pagination

                Debug.WriteLine($"Retrieved {posts.Count()} posts from database.");
                return Result.Ok(posts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetAll: {ex.Message}");
                return Result.Fail($"Error retrieving posts: {ex.Message}");
            }
        }

        public Result<Post> GetPostByCommentId(long commentId)
        {
            var post = _context.Posts
                .AsEnumerable() 
                .FirstOrDefault(p => p.Comments.Any(c => c.Id == commentId));

            return post != null ? Result.Ok(post) : Result.Fail("Post not found");
        }

        
    }
}
