using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/tourPurchaseToken")]
    public class TourPurchaseTokenController:BaseApiController
    {
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;

        public TourPurchaseTokenController(ITourPurchaseTokenService tourPurchaseTokenService)
        {
            _tourPurchaseTokenService = tourPurchaseTokenService;
        }

        [HttpPost]
        public ActionResult<PagedResult<TourPurchaseTokenDto>> Create()
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
            {
                var result = _tourPurchaseTokenService.Create(touristId);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("Invalid input");
            }
        }
    }
}
