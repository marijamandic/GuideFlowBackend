using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/bundles")]
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
            var result = _tourBundleService.GetPopulated(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<TourBundleDto> GetAllPublished()
        {
            var result = _tourBundleService.GetAllPublished();
            return CreateResponse(result);
        }
    }
}
