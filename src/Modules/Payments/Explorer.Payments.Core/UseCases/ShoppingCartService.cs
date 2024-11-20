using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper) : base(mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    public Result<PagedResult<ItemDto>> AddToCart(int touristId, InputDto itemInput)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);

            var item = new ItemDto
            {
                ShoppingCartId = (int)shoppingCart.Id,
                Type = itemInput.Type,
                ProductId = itemInput.ProductId,
                ProductName = itemInput.ProductName,
                AdventureCoin = itemInput.AdventureCoin
            };

            shoppingCart.AddToCart(_mapper.Map<Item>(item));
            _shoppingCartRepository.Save(shoppingCart);

            var items = shoppingCart.Items.Select(i => _mapper.Map<ItemDto>(i)).ToList();
            return new PagedResult<ItemDto>(items, items.Count);
        }
        catch (Exception e)
        {
            return Result.Fail($"Error: {e.Message}");
        }
    }

    public Result RemoveFromCart(int touristId, int itemId)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);
            shoppingCart.RemoveFromCart(shoppingCart.GetById(itemId));
            _shoppingCartRepository.Save(shoppingCart);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail($"Error: {e.Message}");
        }
    }

    public Result ClearCart(int touristId)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);
            shoppingCart.ClearCart();
            _shoppingCartRepository.Save(shoppingCart);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail($"Error: {e.Message}");
        }
    }

    public Result<ShoppingCartDto> GetByTouristId(int touristId)
    {
        return MapToDto(_shoppingCartRepository.GetByTouristId(touristId));
    }
}
