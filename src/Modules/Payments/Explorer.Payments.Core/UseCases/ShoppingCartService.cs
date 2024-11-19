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

    public Result<PagedResult<SingleItemDto>> AddToCart(int touristId, SingleItemInputDto itemInput)
    {
        var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);

        var item = new SingleItemDto
        {
            ShoppingCartId = (int)shoppingCart.Id,
            TourId = itemInput.TourId,
            TourName = itemInput.TourName,
            AdventureCoin = itemInput.AdventureCoin
        };

        try
        {
            shoppingCart.AddToCart(_mapper.Map<SingleItem>(item));
            _shoppingCartRepository.Save(shoppingCart);

            var items = shoppingCart.SingleItems.Select(i => _mapper.Map<SingleItemDto>(i)).ToList();
            return new PagedResult<SingleItemDto>(items, items.Count);
        }
        catch (Exception)
        {
            return Result.Fail("Item already in cart");
        }
    }

    public Result RemoveFromCart(int touristId, int itemId)
    {
        var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);
        shoppingCart.RemoveFromCart(shoppingCart.GetById(itemId));
        _shoppingCartRepository.Save(shoppingCart);
        return Result.Ok();
    }

    public Result ClearCart(int touristId)
    {
        var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);
        shoppingCart.ClearCart();
        _shoppingCartRepository.Save(shoppingCart);
        return Result.Ok();
    }

    public Result<ShoppingCartDto> GetByTouristId(int touristId)
    {
        return MapToDto(_shoppingCartRepository.GetByTouristId(touristId));
    }
}
