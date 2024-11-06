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
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);
        Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId);
        Result<ShoppingCartDto> GetByUserId(long touristId);
    }
}