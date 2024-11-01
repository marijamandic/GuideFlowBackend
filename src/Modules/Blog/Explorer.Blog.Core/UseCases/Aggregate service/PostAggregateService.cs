using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System.Collections.Generic;

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

        public Result<PostDto> CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            var result = _repository.Create(post); 

            if (result.IsSuccess)
            {
                var createdPostDto = _mapper.Map<PostDto>(result.Value);
                return Result.Ok(createdPostDto);
            }

            return Result.Fail("Failed to create post.");
        }

        public Result<PostDto> GetPostById(long postId)
        {
            var result = _repository.GetById(postId); 

            if (result.IsSuccess && result.Value != null)
            {
                var postDto = _mapper.Map<PostDto>(result.Value);
                return Result.Ok(postDto);
            }

            return Result.Fail("Post not found.");
        }

        public Result<IEnumerable<PostDto>> GetAllPosts(int pageNumber, int pageSize)
        {
            var result = _repository.GetAll(pageNumber, pageSize); 

            if (result.IsSuccess)
            {
                var postDtos = _mapper.Map<IEnumerable<PostDto>>(result.Value);
                return Result.Ok(postDtos);
            }

            return Result.Fail("Failed to retrieve posts.");
        }

        public Result AddComment(long postId, CommentDto commentDto)
        {
            var result = _repository.GetById(postId); 

            if (result.IsFailed || result.Value == null)
            {
                return Result.Fail("Post not found.");
            }

            var post = result.Value;
            var comment = _mapper.Map<Comment>(commentDto);
            var addCommentResult = post.AddComment(comment.UserId, postId, comment.CreatedAt, comment.Content, comment.LastModified);

            if (addCommentResult.IsSuccess)
            {
                _repository.Update(post); 
                return Result.Ok();
            }

            return Result.Fail("Failed to add comment.");
        }



        public Result AddRating(long postId, BlogRatingDto blogRatingDto)
        {
            //1. Učitavanje agregata iz repozitorijuma
            var result = _repository.GetById(postId);

            if(result.IsFailed || result.Value == null)
            {
                return Result.Fail("Post not found.");
            }
            var post = result.Value;

            //2. Poziv metode agregata da odgovori na pitanje ili izmeni stanje
            var addRatingResult = post.AddRating(blogRatingDto);

            if (addRatingResult.IsSuccess)
            {
                //3. Čuvanje izmene agregata, ako mu se stanje promenilo,
                _repository.Update(post);
                return Result.Ok();
            }

            //4. Formiranje rezultata operacije koji se vraća kontroleru.
            return Result.Fail("Failed to add rating.");
        }

        public Result DeletePost(long postId)
        {
            var result = _repository.Delete(postId); 
            return result.IsSuccess ? Result.Ok() : Result.Fail("Failed to delete post.");
        }
    }
}
