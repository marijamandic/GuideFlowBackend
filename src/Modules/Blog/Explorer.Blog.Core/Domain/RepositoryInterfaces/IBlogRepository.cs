using Explorer.Blog.Core.Domain.Posts;
using FluentResults;
using System.Collections.Generic;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogRepository
    {
        Result<Post> Create(Post post);
        Result<Post> GetById(long postId);
        Result<Post> Update(Post post);
        Result Delete(long postId);
        Result<IEnumerable<Post>> GetAll(int pageNumber, int pageSize);
        public Result<Post> GetPostByCommentId(long commentId);
    }
}
