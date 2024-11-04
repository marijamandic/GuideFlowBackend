using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System.Collections.Generic;
using System.Xml.Linq;

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
        public Result<int> GetCommentCountForPost(int postId);
        public Result<List<CommentDto>> GetCommentsForPost(long postId);
        Result AddComment(long postId, CommentDto commentDto);
        Result UpdateComment(long postId, CommentDto commentDto);
        public Result DeleteComment(long commentId);

        // Rating operations
        Result AddRating(long postId, BlogRatingDto blogRatingDto);
    }
}
