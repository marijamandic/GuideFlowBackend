using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping;

[Authorize(Policy = "touristPolicy")]
[Route("api/shopping-cart")]
public class ShoppingCartController : BaseApiController
{
    private readonly IShoppingCartService _shoppingCartService;
    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpPost("items")]
    public ActionResult<ItemDto> AddToCart([FromBody] ItemInputDto item)
    {
        int touristId = int.Parse(User.FindFirst("id")!.Value);
        var result = _shoppingCartService.AddToCart(touristId, item);
        return CreateResponse(result);
    }

    [HttpDelete("items/{itemId:int}")]
    public ActionResult RemoveFromCart([FromRoute] int itemId)
    {
        int touristId = int.Parse(User.FindFirst("id")!.Value);
        var result = _shoppingCartService.RemoveFromCart(touristId, itemId);
        return CreateResponse(result);
    }

    [HttpGet]
    public ActionResult<ShoppingCartDto> GetByTouristId()
    {
        if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
        {
            var result = _shoppingCartService.GetByTouristId(touristId);
            return CreateResponse(result);
        }

        return Unauthorized();
    }

	[HttpGet("populated")]
    public ActionResult<ShoppingCartDto> GetPopulatedByTouristId()
    {
		if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
		{
			var result = _shoppingCartService.GetPopulatedByTouristId(touristId);
			return CreateResponse(result);
		}

		return Unauthorized();
    }

    [HttpPut("items/{itemId:int}")]
    public ActionResult<ItemDto> UpdateShoppingCart(int itemId, [FromBody] ItemInputDto updatedItemDto)
    {
        int touristId = int.Parse(User.FindFirst("id")!.Value);

        if (updatedItemDto == null)
        {
            return BadRequest("Updated item data is required.");
        }

        var result = _shoppingCartService.UpdateShoppingCart(touristId, itemId, updatedItemDto);

        if (result.IsFailed)
        {
            var errorMessages = string.Join("; ", result.Errors.Select(e => e.Message));
            return StatusCode(500, new { Message = errorMessages });
        }

        return Ok(result.Value);
    }

}
