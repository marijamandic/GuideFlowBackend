using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface ICommentService
    {
        //public Result<List<CommentDto>> GetAllByPost(int id); mozda
        Result<PagedResult<CommentDto>> GetPaged(int page, int pageSize);//iz ovoga svakako mogu dobiti sve komentare ako stavim page=0,pageSize=0
        Result<CommentDto> Create(CommentDto comment);
        Result<CommentDto> Update(CommentDto comment);
        Result Delete(int id);
    }
}
