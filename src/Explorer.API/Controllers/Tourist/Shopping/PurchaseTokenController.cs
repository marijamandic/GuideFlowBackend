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
    public class PurchaseTokenController:BaseApiController
    {
        private readonly IPurchaseTokenService _purchaseTokenService;

        public PurchaseTokenController(IPurchaseTokenService purchaseTokenService)
        {
            _purchaseTokenService = purchaseTokenService;
        }

        [HttpPost]
        public ActionResult<PagedResult<PurchaseTokenDto>> Create()
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
            {
                var result = _purchaseTokenService.Create(touristId);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("Invalid input");
            }
        }
    }
}
