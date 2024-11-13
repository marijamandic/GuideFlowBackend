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

        public Result<PostDto> CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            var result = _repository.Create(post);
            var createdPostDto = _mapper.Map<PostDto>(post);
            return result.IsSuccess ? Result.Ok(createdPostDto) : Result.Fail("Failed to create post.");
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

        public Result<CommentDto> AddComment(long postId, CommentDto commentDto)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var addCommentResult = postResult.Value.AddComment(
                commentDto.UserId,
                postId,
                commentDto.CreatedAt,
                commentDto.Content,
                commentDto.LastModified
            );

            if (addCommentResult.IsFailed)
                return Result.Fail("Failed to add comment.");

            _repository.Update(postResult.Value);
            var createdCommentDto = _mapper.Map<CommentDto>(addCommentResult.Value);

            return Result.Ok(createdCommentDto);
        }

        public Result<CommentDto> UpdateComment(long postId, CommentDto commentDto)
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
            var updatedCommentDto = _mapper.Map<CommentDto>(updateResult.Value);
            return updateRepoResult.IsSuccess ? Result.Ok(updatedCommentDto) : Result.Fail("Failed to update repository.");
        }

        public Result DeleteComment(long commentId)
        {
            var postResult = _repository.GetPostByCommentId(commentId);  
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var deleteResult = postResult.Value.DeleteComment(commentId);
            if (deleteResult.IsFailed && deleteResult.Errors.Any(e => e.Message == "Comment not found"))
            {
                Console.WriteLine("Comment not found for deletion.");
                return Result.Fail("Comment not found"); // Return specific message for controller to handle 404
            }

            if (deleteResult.IsFailed)
            {
                Console.WriteLine("Failed to delete comment due to an unknown error.");
                return Result.Fail("Failed to delete comment.");
            }

            // Update repository and ensure changes are saved
            var updateRepoResult = _repository.Update(postResult.Value);
            return updateRepoResult.IsSuccess ? Result.Ok() : Result.Fail("Failed to update repository after deletion.");
        }

        public Result<BlogRatingDto> AddRating(long postId, BlogRatingDto blogRatingDto)
        {
            var result = _repository.GetById(postId);
            if (result.IsFailed || result.Value == null)
                return Result.Fail("Post not found.");

            var post = result.Value;
            blogRatingDto.RatingDate = DateTime.Now;
            var addRatingResult = post.AddRating(blogRatingDto);

            if (addRatingResult.IsFailed) return Result.Fail("Failed to add rating.");

            post.UpdateEngagementStatus(); // update status
            var addedRatingResult = _mapper.Map<BlogRatingDto>(addRatingResult.Value);

            _repository.Update(post);
            return Result.Ok(addedRatingResult);
        }


        public Result<List<BlogRatingDto>> GetRatingsForPost(long postId)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
            {
                Debug.WriteLine("#### ERROR: Post not found ####");
                return Result.Fail("Post not found.");
            }

            try
            {
                var ratings = postResult.Value.Ratings.ToList();
                var ratingDtos = _mapper.Map<List<BlogRatingDto>>(ratings);
                return Result.Ok(ratingDtos);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"#### ERROR in GetRatingsForPost: {ex.Message} ####");
                return Result.Fail("Error retrieving ratings.");
            }
        }

        public Result DeleteRating(long userId, long postId)
        {
            // Retrieve the post by postId
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
            {
                Debug.WriteLine("#### ERROR: Post not found ####");
                return Result.Fail("Post not found.");
            }

            var post = postResult.Value;

            
            var deleteRatingResult = post.DeleteRating(userId, postId);
            if (deleteRatingResult.IsFailed)
            {
                Debug.WriteLine("#### ERROR: " + deleteRatingResult.Errors[0].Message + " ####");
                return Result.Fail("Failed to delete rating.");
            }

            var updateRepoResult = _repository.Update(post);
            return updateRepoResult.IsSuccess ? Result.Ok() : Result.Fail("Failed to update repository after deleting rating.");
        }


        // EngagementStatus operations
        public Result<int> GetEngagementStatus(long postId)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var post = postResult.Value;

            post.UpdateEngagementStatus();

            var updateResult = _repository.Update(post);
            if (updateResult.IsFailed)
                return Result.Fail("Failed to update engagement status.");

            int statusCode = (int)post.EngagementStatus;
            return Result.Ok(statusCode);
        }


        public Result UpdateEngagementStatus(long postId)
        {
            var postResult = _repository.GetById(postId);
            if (postResult.IsFailed || postResult.Value == null)
                return Result.Fail("Post not found.");

            var post = postResult.Value;
            post.UpdateEngagementStatus(); // Ažurira EngagementStatus

            var updateResult = _repository.Update(post);
            return updateResult.IsSuccess ? Result.Ok() : Result.Fail("Failed to update post engagement status.");
        }
    }

    public enum EngagementStatus
    {
        Inactive,
        Active,
        Famous,
        Closed
    }
}
