using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.CommentView
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/commentview/comment")]
    public class CommentController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;

        public CommentController(IPostAggregateService postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        [HttpGet]
        public ActionResult<List<CommentDto>> GetAllForPost([FromQuery] int postId)
        {
            var result = _postAggregateService.GetCommentsForPost(postId);
            return CreateResponse(result);
        }
    }
}
