using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<PagedResult<ItemDto>> AddToCart(int touristId, ItemInputDto item);
    Result RemoveFromCart(int touristId, int itemId);
    Result ClearCart(int touristId);
    Result<ShoppingCartDto> GetByTouristId(int touristId);
}
