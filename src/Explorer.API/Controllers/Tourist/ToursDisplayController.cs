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

            if (result == null)
            {
                return Ok(new { message = "There is no purchased tours yet" });
            }

            return CreateResponse(result);
        }

        [HttpGet("purchased/{userId:int}/{tourId:int}")]
        public ActionResult<IEnumerable<TourDto>> CheckIfPurchased(int userId, int tourId)
        {
            var result = _tourService.CheckIfPurchased(userId, tourId);

            if (result == null)
            {
                return Ok(null);
            }

            return CreateResponse(result);
        }

        [HttpGet("bundle/{id:int}")]
        public ActionResult<PagedResult<TourDto>> GetToursByBundleId(int id)
        {
            var result = _tourService.GetToursByBundleId(id);
            return CreateResponse(result);
        }

        [HttpGet("suggested/{longitude:double}/{latitude:double}")]
        public ActionResult<List<TourDto>> GetSuggestedTours(double longitude, double latitude)
        {
            var result = _tourService.GetSuggestedTours(longitude, latitude);
            return CreateResponse(result);
        }
    }
}
