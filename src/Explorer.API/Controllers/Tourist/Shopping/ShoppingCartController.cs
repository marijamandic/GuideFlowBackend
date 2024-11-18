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
    public ActionResult<PagedResult<SingleItemDto>> AddToCart([FromBody] SingleItemInputDto item)
    {
        int touristId = int.Parse(User.FindFirst("id")!.Value);
        var result = _shoppingCartService.AddToCart(touristId, item);
        return CreateResponse(result);
    }
}
