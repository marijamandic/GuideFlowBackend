using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Route("api/purchaseTokens")] // Ruta za PurchaseToken entitete
    [ApiController]
    public class PurchaseTokenController : BaseApiController
    {
        private readonly IPurchaseTokensService _purchaseTokensService;

        public PurchaseTokenController(IPurchaseTokensService purchaseTokensService)
        {
            _purchaseTokensService = purchaseTokensService;
        }

        // GET: api/purchaseTokens
        [HttpGet]
        public ActionResult<PagedResult<PurchaseTokenDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            if (page < 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be non-negative, and pageSize must be greater than zero.");
            }

            var result = _purchaseTokensService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        // GET: api/purchaseTokens/{id}
        [HttpGet("{id:int}")]
        public ActionResult<PurchaseTokenDto> GetPurchaseToken(int id)
        {
            var result = _purchaseTokensService.Get(id);
            return CreateResponse(result);
        }

        // POST: api/purchaseTokens
        [HttpPost]
        public ActionResult<PurchaseTokenDto> Create([FromBody] PurchaseTokenDto purchaseToken)
        {
            // Validacija: proveri da li su UserId i TourId validni
            if (purchaseToken.UserId <= 0 || purchaseToken.TourId <= 0)
            {
                return BadRequest("Invalid UserId or TourId. Both values must be greater than zero.");
            }

            // Ako su vrednosti validne, pozovi servis za kreaciju
            var result = _purchaseTokensService.Create(purchaseToken);

            // Vrati rezultat koristeći postojeću metodu CreateResponse
            return CreateResponse(result);
        }

        // PUT: api/purchaseTokens/{id}
        [HttpPut("{id:int}")]
        public ActionResult<PurchaseTokenDto> Update(int id, [FromBody] PurchaseTokenDto purchaseToken)
        {
            if (id != purchaseToken.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }

            var result = _purchaseTokensService.Update(purchaseToken);
            return CreateResponse(result);
        }

        // DELETE: api/purchaseTokens/{id}
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _purchaseTokensService.Delete(id);
            return CreateResponse(result);
        }

        // GET: api/purchaseTokens/user/{userId}
        [HttpGet("user/{userId:int}")]
        public ActionResult<PagedResult<PurchaseTokenDto>> GetTokensByUserId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _purchaseTokensService.GetTokensByUserId(userId);
            return CreateResponse(result);
        }
    }
}
