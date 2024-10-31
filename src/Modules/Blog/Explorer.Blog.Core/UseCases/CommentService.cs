using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService:CrudService<CommentDto,Comment>,ICommentService
    {
        public CommentService(ICrudRepository<Comment> repository,IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<CommentDto>> GetAllForPost(int id,int page, int pageSize)
        {
            var comments = GetPaged(page, pageSize);

            if (comments.IsFailed)
            {
                return Result.Fail<PagedResult<CommentDto>>(comments.Errors);
            }

            var commentsForPost = comments.Value.Results.FindAll(c => c.PostId == id);
            var pagedResult = new PagedResult<CommentDto>(commentsForPost, commentsForPost.Count);

            return Result.Ok(pagedResult);
        }
    }
}
