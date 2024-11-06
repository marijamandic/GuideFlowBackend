using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/problems")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpPost]
        public ActionResult<ProblemDto> Create([FromBody] ProblemDto problem)
        {
            var result = _problemService.Create(problem);
            return CreateResponse(result);
        }
        /*[HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll()
        {
            var result = _problemService.GetAll();
            return CreateResponse(result);
        }*/
        [HttpPut("{id:int}")]
        public ActionResult<ProblemDto> Update(int id, [FromBody] ProbStatusChangeDto status)
        {
            var result = _problemService.Update(status,id);
            return CreateResponse(result);
        }
        [HttpGet("{userId:int}")]
        public ActionResult<PagedResult<ProblemDto>> GetAll(int userId)
        {
            var result = _problemService.GetUserProblems(userId);
            return CreateResponse(result);
        }
    }
}
