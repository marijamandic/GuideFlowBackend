using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/tourExecution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
        }
        [HttpPost]
        public ActionResult<TourExecutionDto> Create([FromBody] CreateTourExecutionDto createTourExecutionDto) {
            var result = _tourExecutionService.Create(createTourExecutionDto);
            return CreateResponse(result);
        }
        [HttpPut]
        public ActionResult<TourExecutionDto> Update([FromBody] UpdateTourExecutionDto updateTourExecutionDto) {
            var result = _tourExecutionService.Update(updateTourExecutionDto);
            return CreateResponse(result);
        }

        [HttpGet("{userId:long}")]
        public ActionResult<TourExecutionDto> GetByUser(long userId)
        {
            var result = _tourExecutionService.GetSessionsByUserId(userId);
            return CreateResponse(result);
        }

        // za swagger

        [HttpPut("complete/{userId}")]
        public IActionResult CompleteTour(long userId)
        {
            try
            {
                _tourExecutionService.CompleteSession(userId);
                return Ok("Tour execution completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("abandon/{userId}")]
        public IActionResult AbandonTour(long userId)
        {
            try
            {
                _tourExecutionService.AbandonSession(userId);
                return Ok("Tour execution abandoned successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
