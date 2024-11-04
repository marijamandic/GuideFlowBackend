using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.CommentManaging
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/commentmanaging/comment")]
    public class CommentController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;
        private readonly IUserService _userService;

        public CommentController(IPostAggregateService postAggregateService, IUserService userService)
        {
            _postAggregateService = postAggregateService;
            _userService = userService;
        }

        [HttpGet("all")]
        public ActionResult<List<CommentDto>> GetAllCommentsForPost([FromQuery] long postId)
        {
            var result = _postAggregateService.GetCommentsForPost(postId);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }

        [HttpGet("user/{id:int}")]
        public ActionResult<UserDto> GetCommentCreator(int id)
        {
            var result = _userService.GetById(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CommentDto comment)
        {
            var result = _postAggregateService.AddComment(comment.PostId, comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update([FromBody] CommentDto comment, int id)
        {
            comment.Id = id;
            var result = _postAggregateService.UpdateComment(comment.PostId, comment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _postAggregateService.DeleteComment(id);
            return CreateResponse(result);
        }

        [HttpGet("count")]
        public ActionResult<int> GetCommentCount([FromQuery] int postId)
        {
            var result = _postAggregateService.GetCommentCountForPost(postId);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }

    }
}
