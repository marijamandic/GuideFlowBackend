using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System.Collections.Generic;
using System.Diagnostics;

namespace Explorer.Blog.Core.UseCases.Aggregate_service
{
    public class PostAggregateService : IPostAggregateService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public PostAggregateService(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Post operations
        public Result<List<PostDto>> GetAllPosts(int pageNumber, int pageSize)
        {
            var postsResult = _repository.GetAll(pageNumber, pageSize);

            if (postsResult.IsFailed)
            {
                Debug.WriteLine("#### ERROR: Failed to retrieve posts ####");
                return Result.Fail("Failed to retrieve posts.");
            }
            var postDtos = _mapper.Map<List<PostDto>>(postsResult.Value.ToList());
            return Result.Ok(postDtos);
        }

        public Result<PostDto> GetPostById(long postId)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var postDto = _mapper.Map<PostDto>(postResult.Value);
            return Result.Ok(postDto);
        }

        public Result CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            var result = _repository.Create(post);
            return result.IsSuccess ? Result.Ok() : Result.Fail("Failed to create post.");
        }

        public Result UpdatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            var result = _repository.Update(post);
            return result.IsSuccess ? Result.Ok() : Result.Fail("Failed to update post.");
        }

        public Result DeletePost(long postId)
        {
            var result = _repository.Delete(postId);
            return result.IsSuccess ? Result.Ok() : Result.Fail("Failed to delete post.");
        }

        // Comment operations
        public Result<int> GetCommentCountForPost(int postId)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            // Return only the count of comments
            int commentCount = postResult.Value.Comments.Count;
            return Result.Ok(commentCount);
        }

        public Result<List<CommentDto>> GetCommentsForPost(long postId)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
            {
                Debug.WriteLine("#### ERROR: Post not found ####");
                return Result.Fail("Post not found.");
            }

            try
            {
                var comments = postResult.Value.Comments.ToList(); // Retrieve all comments
                var commentDtos = _mapper.Map<List<CommentDto>>(comments);
                return Result.Ok(commentDtos);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"#### ERROR in GetCommentsForPost: {ex.Message} ####");
                return Result.Fail("Error retrieving comments.");
            }
        }

        public Result AddComment(long postId, CommentDto commentDto)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var addCommentResult = postResult.Value.AddComment(
                commentDto.Id,  
                commentDto.UserId,
                postId,
                commentDto.CreatedAt,
                commentDto.Content,
                commentDto.LastModified
            );

            if (addCommentResult.IsFailed)
                return Result.Fail("Failed to add comment.");

            _repository.Update(postResult.Value);
            return Result.Ok();
        }

        public Result UpdateComment(long postId, CommentDto commentDto)
        {
            Debug.WriteLine($"Updating comment for postId: {postId}, commentId: {commentDto.Id}");
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
            {
                Debug.WriteLine("Post not found.");
                return Result.Fail("Post not found.");
            }

            var updateResult = postResult.Value.UpdateComment(_mapper.Map<Comment>(commentDto));
            if (updateResult.IsFailed)
            {
                Debug.WriteLine("Failed to update comment.");
                return Result.Fail("Failed to update comment.");
            }

            var updateRepoResult = _repository.Update(postResult.Value);
            return updateRepoResult.IsSuccess ? Result.Ok() : Result.Fail("Failed to update repository.");
        }

        public Result DeleteComment(long commentId)
        {
            var postResult = _repository.GetPostByCommentId(commentId);  
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var deleteResult = postResult.Value.DeleteComment(commentId);
            if (deleteResult.IsFailed) return Result.Fail("Failed to delete comment.");

            var updateRepoResult = _repository.Update(postResult.Value);
            return updateRepoResult.IsSuccess ? Result.Ok() : Result.Fail("Failed to update repository after deletion.");
        }

        public Result AddRating(long postId, BlogRatingDto blogRatingDto)
        {
            var result = _repository.GetById(postId);
            if (result.IsFailed || result.Value == null)
                return Result.Fail("Post not found.");

            var post = result.Value;
            var addRatingResult = post.AddRating(blogRatingDto);
            if (addRatingResult.IsFailed) return Result.Fail("Failed to add rating.");

            _repository.Update(post);
            return Result.Ok();
        }
    }
}
