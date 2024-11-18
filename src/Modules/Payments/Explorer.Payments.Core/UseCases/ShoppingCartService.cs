using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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
            Price = itemInput.Price
        };
        shoppingCart.AddToCart(_mapper.Map<SingleItem>(item));
        _shoppingCartRepository.Save(shoppingCart);

        var items = shoppingCart.SingleItems.Select(i => _mapper.Map<SingleItemDto>(i)).ToList();
        return new PagedResult<SingleItemDto>(items, items.Count);
    }

    public Result<ShoppingCartDto> GetByTouristId(int touristId)
    {
        return MapToDto(_shoppingCartRepository.GetByTouristId(touristId));
    }
}
