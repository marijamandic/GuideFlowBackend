using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogDatabaseRepository : IBlogRepository
    {
        private readonly BlogContext _dbContext;

        public BlogDatabaseRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post?> GetByIdAsync(long postId)
        {
            return await _dbContext.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.Comments)      
                .Include(p => p.Ratings)       
                .FirstOrDefaultAsync();
        }

        public async Task<Post> CreateAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);

            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> UpdateAsync(Post post)
        {
            _dbContext.Entry(post).State = EntityState.Modified;

            foreach (var comment in post.Comments)
            {
                _dbContext.Entry(comment).State = EntityState.Modified;
            }

            foreach (var rating in post.Ratings)
            {
                _dbContext.Entry(rating).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task DeleteAsync(long postId)
        {
            var post = await _dbContext.Posts.FindAsync(postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post); 
                await _dbContext.SaveChangesAsync(); 
            }
        }

        public List<BlogRating>? GetAll()
        {
            return null;
        }
    }
}
