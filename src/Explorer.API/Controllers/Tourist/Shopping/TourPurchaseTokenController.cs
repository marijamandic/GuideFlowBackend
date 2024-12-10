using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/tourPurchaseToken")]
    public class TourPurchaseTokenController : BaseApiController
    {
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;

        public TourPurchaseTokenController(ITourPurchaseTokenService tourPurchaseTokenService)
        {
            _tourPurchaseTokenService = tourPurchaseTokenService;
        }

        [HttpGet("tour/{id:int}")]
        public ActionResult<TourPurchaseTokenDto> GetTokenByTouristAndTourId(int id)
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
            {
                var result = _tourPurchaseTokenService.GetTokenByTouristAndTourId(touristId,id);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("Invalid input");
            }
        }
    }
}
