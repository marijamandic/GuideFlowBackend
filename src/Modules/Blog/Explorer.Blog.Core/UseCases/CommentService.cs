using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
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

        /*public Result<List<CommentDto>> GetAllByPost(int id)
        {
            List<CommentDto> comments = GetPaged(0, 0).ValueOrDefault.Results;
            return Result.Ok<List<CommentDto>>(comments.FindAll(c=>c.PostId==id));
        }*/
    }
}
