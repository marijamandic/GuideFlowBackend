using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<PagedResult<SingleItemDto>> AddToCart(int touristId, SingleItemInputDto item);
    Result<ShoppingCartDto> GetByTouristId(int touristId);
}
