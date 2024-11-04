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
    }
}
