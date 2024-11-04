using Explorer.Blog.API.Dtos;
using FluentResults;
using System.Collections.Generic;

namespace Explorer.Blog.API.Public.Aggregate_service_interface
{
    public interface IPostAggregateService
    {
        // Post operations
        Result<List<PostDto>> GetAllPosts(int pageNumber, int pageSize);
        Result<PostDto> GetPostById(long postId);
        Result CreatePost(PostDto postDto);
        Result UpdatePost(PostDto postDto);
        Result DeletePost(long postId);

        // Comment operations
        Result<int> GetCommentCountForPost(int postId);
        Result<List<CommentDto>> GetCommentsForPost(long postId);
        Result AddComment(long postId, CommentDto commentDto);
        Result UpdateComment(long postId, CommentDto commentDto);
        Result DeleteComment(long commentId);

        // Rating operations
        Result AddRating(long postId, BlogRatingDto blogRatingDto);
        Result<List<BlogRatingDto>> GetRatingsForPost(long postId);
        Result DeleteRating(long userId, long postId);

        // EngagementStatus operations
        Result<int> GetEngagementStatus(long postId);
        Result UpdateEngagementStatus(long postId);
    }
}
