using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.CommentView
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/commentview/comment")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CommentDto>> GetAllForPost([FromQuery] int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = commentService.GetAllForPost(id, page, pageSize);
            return CreateResponse(result);
        }
    }
}
