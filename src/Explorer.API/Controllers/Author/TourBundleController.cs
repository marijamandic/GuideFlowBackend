using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Explorer.API.Controllers.Author
{
   // [Authorize(Policy = "authorPolicy")]
    [Route("api/author/tourBundlesManagement")]
    public class TourBundleController : BaseApiController
    {
        private readonly ITourBundleService _tourBundleService;

        public TourBundleController(ITourBundleService tourBundleService)
        {
            _tourBundleService = tourBundleService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourBundleDto>> GetAll() 
        {
            var result = _tourBundleService.GetAll();
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourBundleDto> Create([FromBody] TourBundleDto tourBundleDto)
        {
            var result = _tourBundleService.Create(tourBundleDto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public ActionResult<TourBundleDto> Delete(long tourBundleId)
        {
            var result = _tourBundleService.Delete(tourBundleId);
            return CreateResponse(result);
        }

        [HttpPatch("addTour")]
        public ActionResult<TourBundleDto> AddTour(long tourBundleId, long tourId)
        {
            var result = _tourBundleService.AddTour(tourBundleId, tourId);
            return CreateResponse(result);
        }

        [HttpPatch("removeTour")]
        public ActionResult<TourBundleDto> RemoveTour(long tourBundleId, long tourId)
        {
            var result = _tourBundleService.RemoveTour(tourBundleId, tourId);
            return CreateResponse(result);
        }

        [HttpPatch("publish")]
        public ActionResult<TourBundleDto> Publish(long tourBundleId)
        {
            var result = _tourBundleService.Publish(tourBundleId);
            return CreateResponse(result);
        }

        [HttpPatch("archive")]
        public ActionResult<TourBundleDto> Archieve(long tourBundleId)
        {
            var result = _tourBundleService.Archive(tourBundleId);
            return CreateResponse(result);
        }

    }
}
