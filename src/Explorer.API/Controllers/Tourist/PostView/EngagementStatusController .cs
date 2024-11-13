using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Explorer.API.Controllers.Tourist.PostView
{
    [Route("api/postview/engagement")]
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
            Debug.WriteLine($"Received request for engagement status with postId: {postId}"); 

            var result = _postAggregateService.GetEngagementStatus(postId);
            Debug.WriteLine($"Result from _postAggregateService: IsSuccess = {result.IsSuccess}, Value = {result.Value}, Errors = {result.Errors}");

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
