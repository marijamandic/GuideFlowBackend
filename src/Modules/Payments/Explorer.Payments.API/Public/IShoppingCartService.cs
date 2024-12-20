using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<ItemDto> AddToCart(int touristId, ItemInputDto item);
    Result RemoveFromCart(int touristId, int itemId);
    Result ClearCart(int touristId);
    Result<ShoppingCartDto> GetByTouristId(int touristId);
    Result<ShoppingCartDto> GetPopulatedByTouristId(int touristId);
    Result<ItemDto> UpdateShoppingCart(int itemId, ItemInputDto updatedItemDto);
}
