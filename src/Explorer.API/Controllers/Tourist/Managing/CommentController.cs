using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Managing
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/managing/comment")]
    public class CommentController : BaseApiController
    {
        ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CommentDto>> GetAll(int page, int pageSize)
        {
            var result = commentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CommentDto> Create([FromBody] CommentDto comment)
        {
            var result = commentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommentDto> Update([FromBody] CommentDto comment)
        {
            var result = commentService.Update(comment);
            return CreateResponse(result);  
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result=commentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
