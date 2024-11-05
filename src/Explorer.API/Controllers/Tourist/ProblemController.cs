using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/problems")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpPost]
        public ActionResult<ProblemDto> Create([FromBody] CreateProblemInputDto problemInput)
        {
            var result = _problemService.Create(problemInput);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetByTouristId()
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
            {
                var result = _problemService.GetByTouristId(touristId);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("Invalid input");
            }
        }

        [HttpPost("messages")]
        public ActionResult<PagedResult<MessageDto>> CreateMessage([FromBody] CreateMessageInputDto messageInput)
        {
            bool isBadRequest = !int.TryParse(User.FindFirst("id")?.Value, out int authorId) ||
                string.IsNullOrWhiteSpace(messageInput.Content);

            if (isBadRequest) return BadRequest("Invalid input");

            var result = _problemService.CreateMessage(authorId, messageInput);
            return CreateResponse(result);
        }
    }
}
