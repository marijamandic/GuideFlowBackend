using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/tourExecution")]
    public class ToursDisplayController : BaseApiController
    {
        private readonly ITourService _tourService;

        public ToursDisplayController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("purchased/{userId:int}")]
        public ActionResult<IEnumerable<TourDto>> GetPurchasedAndArchivedByUser(int userId)
        {
            var result = _tourService.GetPurchasedAndArchivedByUser(userId);
            return CreateResponse(result);
        }
    }
}
