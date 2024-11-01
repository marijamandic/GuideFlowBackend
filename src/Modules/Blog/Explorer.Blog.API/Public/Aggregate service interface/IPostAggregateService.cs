using Explorer.Blog.API.Dtos;
using FluentResults;
using System.Collections.Generic;

namespace Explorer.Blog.API.Public.Aggregate_service_interface
{
    public interface IPostAggregateService
    {
        Result<PostDto> CreatePost(PostDto postDto);

        Result<PostDto> GetPostById(long postId);

        Result<IEnumerable<PostDto>> GetAllPosts(int pageNumber, int pageSize);

        Result AddComment(long postId, CommentDto commentDto);

        Result AddRating(long postId, BlogRatingDto ratingDto);

        Result DeletePost(long postId);
    }
}
