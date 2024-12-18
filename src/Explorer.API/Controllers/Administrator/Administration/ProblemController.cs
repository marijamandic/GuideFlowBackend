using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/problems")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll()
        {
            var result = _problemService.GetAll();
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/deadline")]
        public ActionResult<ProblemDto> UpdateDeadline(int id, [FromBody] DeadlineDto deadline)
        {
            var jwtUser = new UserDto
            {
                Id = int.Parse(User.FindFirst("id")!.Value),
                Role = (UserRole)Enum.Parse(typeof(UserRole), User.FindFirst(ClaimTypes.Role)!.Value, ignoreCase: true),
                Username = User.FindFirst("username")!.Value
            };

            var result = _problemService.UpdateDeadline(id, deadline.Date, jwtUser);
            return CreateResponse(result);
        }
        [HttpGet("{problemId:int}/problem")]
        public ActionResult<ProblemDto> GetProblemById(int problemId)
        {
            var result = _problemService.GetProblemById(problemId);
            return CreateResponse(result);
        }
    }
}
