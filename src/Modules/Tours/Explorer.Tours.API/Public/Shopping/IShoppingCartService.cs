using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Shopping; 
using FluentResults;
using System;

namespace Explorer.Tours.API.Public
{
    public interface IShoppingCartService
    {
        Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize);
        Result<ShoppingCartDto> Get(int id);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);
        Result<ShoppingCartDto> AddItemToCart(long userId, OrderItemDto orderItemDto);

        Result<ShoppingCartDto> RemoveItemFromCart(long userId, long tourId);
        Result<ShoppingCartDto> GetByUserId(long touristId);

        Result<ShoppingCartDto> ClearCart(long userId);
    }
}