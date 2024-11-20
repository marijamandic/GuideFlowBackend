using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Public;
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
    public ActionResult<PagedResult<ItemDto>> AddToCart([FromBody] InputDto item)
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
        int touristId = int.Parse(User.FindFirst("id")!.Value);
        var result = _shoppingCartService.GetByTouristId(touristId);
        return CreateResponse(result);
    }
}
