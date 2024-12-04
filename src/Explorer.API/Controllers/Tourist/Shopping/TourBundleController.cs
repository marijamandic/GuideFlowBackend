using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/tourBundle")]
    public class TourBundleController : BaseApiController
    {
        private readonly ITourBundleService _tourBundleService;

        public TourBundleController(ITourBundleService tourBundleService)
        {
            _tourBundleService = tourBundleService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourBundleDto> GetById(int id)
        {
            var result = _tourBundleService.Get(id);
            return CreateResponse(result);
        }
    }
}
