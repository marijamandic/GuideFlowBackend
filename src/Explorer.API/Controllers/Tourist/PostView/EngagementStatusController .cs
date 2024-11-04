using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.PostView
{
    [Route("api/posts/engagement")]
    public class EngagementStatusController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;

        public EngagementStatusController(IPostAggregateService postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        [HttpGet("{postId}/status")]
        public ActionResult<int> GetEngagementStatus(long postId)
        {
            var result = _postAggregateService.GetEngagementStatus(postId);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }


        [HttpPost("{postId}/update")]
        public ActionResult UpdateEngagementStatus(long postId)
        {
            var result = _postAggregateService.UpdateEngagementStatus(postId);
            return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
        }
    }
}
