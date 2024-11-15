using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    //[Authorize(Policy = "administratorPolicy")]
    [Route("api/shoppingCart")] // Izmenjena ruta
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ShoppingCartDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _shoppingCartService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        /*
        [HttpGet("{id:int}")]
        public ActionResult<ShoppingCartDto> GetShoppingCart(int id)
        {
            var result = _shoppingCartService.Get(id);
            return CreateResponse(result);
        }
        */
        


        [HttpPut]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Update(shoppingCart);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _shoppingCartService.Delete(id);
            return CreateResponse(result);
        }

        /*
        [HttpPut("addItem/{touristId:long}")]
        public ActionResult<ShoppingCartDto> AddItemToCart(long touristId, [FromBody] OrderItemDto orderItem)
        {
            var result = _shoppingCartService.AddToCart(orderItem, touristId);
            return CreateResponse(result);
        }
        */

        
        [HttpGet("{userId:int}")]
        public ActionResult<ShoppingCartDto> GetCartByUserId(long userId)
        {
            var result = _shoppingCartService.GetByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Vraća 200 OK sa ShoppingCartDto
            }

            return NotFound(result.Errors.First().Message); // Vraća 404 ako korpa nije pronađena
        }
        
        [HttpPost("{userId}/items")]
        public ActionResult AddItemToCart(long userId, [FromBody] OrderItemDto itemDto)
        {
            var result = _shoppingCartService.AddItemToCart(userId, itemDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Vraća 200 OK
            }

            return BadRequest(result.Errors.First().Message); // Vraća 400 Bad Request sa greškom
        }

        [HttpDelete("{userId}/items/{tourId}")]
        public ActionResult RemoveItemFromCart(long userId, int tourId )
        {
            var result = _shoppingCartService.RemoveItemFromCart(userId, tourId);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Vraća 200 OK
            }

            return NotFound(result.Errors.First().Message); // Vraća 404 ako stavka nije pronađena
        }
        /*
        [HttpPut("{userId}/items/{tourID}/quantity")]
        public ActionResult UpdateItemQuantity(string userId, int tourID, [FromBody] int quantity)
        {
            var result = _shoppingCartService.UpdateItemQuantity(userId, tourID, quantity);
            if (result.IsSuccess)
            {
                return Ok(); // Vraća 200 OK
            }

            return NotFound(result.Errors.First().Message); // Vraća 404 ako stavka nije pronađena
        }
        */
        [HttpDelete("{userId}/clear")]
        public ActionResult ClearCart(long userId)
        {
            var result = _shoppingCartService.ClearCart(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Vraća 200 OK
            }

            return NotFound("Cart not found."); // Vraća 404 ako korpa nije pronađena
        }
        
    }
}
