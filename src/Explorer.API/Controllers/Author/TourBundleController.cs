using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;
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

        [HttpGet("cheapestFive")]
        public ActionResult<PagedResult<TourBundleDto>> GetCheapestFive()
        {
            var result = _tourBundleService.GetAll();

            if (!result.IsSuccess)
                return CreateResponse(result);

            // Sort the results by Price in ascending order and take the cheapest five
            var cheapestFive = result.Value.Results
                .OrderBy(bundle => bundle.Price)
                .Take(5)
                .ToList();

            var pagedResult = new PagedResult<TourBundleDto>(cheapestFive, cheapestFive.Count);

            var cheapestFiveResult = Result.Ok(pagedResult);

            return CreateResponse(cheapestFiveResult);
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

        [HttpPut]
        public ActionResult<TourBundleDto> Modify([FromBody] TourBundleDto tourBundleDto)
        {
            var result = _tourBundleService.Modify(tourBundleDto);
            return CreateResponse(result);
        }

    }
}
