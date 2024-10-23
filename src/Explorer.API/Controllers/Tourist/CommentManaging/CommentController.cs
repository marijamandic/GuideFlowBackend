using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
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
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public CommentController(ICommentService commentService,IUserService userService)
        {
            this.commentService = commentService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CommentDto>> GetAllForPost([FromQuery]int id,[FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = commentService.GetAllForPost(id,page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("user/{id:int}")]
        public ActionResult<UserDto> GetCommentCreator(int id) 
        { 
            var result=userService.GetById(id);
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
