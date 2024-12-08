using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IInternalTourService _internalTourService;
    private readonly ITourBundleService _tourBundleService;

	public ShoppingCartService(
		IMapper mapper,
		IShoppingCartRepository shoppingCartRepository,
		IInternalTourService internalTourService,
		ITourBundleService tourBundleService) : base(mapper)
	{
		_shoppingCartRepository = shoppingCartRepository;
		_internalTourService = internalTourService;
		_tourBundleService = tourBundleService;
	}

	public Result<PagedResult<ItemDto>> AddToCart(int touristId, ItemInputDto itemInput)
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

	public Result<PopulatedShoppingCartDto> GetPopulatedByTouristId(int touristId)
	{
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);

            return new PopulatedShoppingCartDto
            {
                Id = (int)shoppingCart.Id,
                TouristId = (int)shoppingCart.TouristId,
                Items = GetPopulatedItemsByIds(shoppingCart.Items.ToList())
            };
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError($"{e.Message}");
        }
	}

    private List<PopulatedItemDto> GetPopulatedItemsByIds(List<Item> items)
    {
        List<PopulatedItemDto> populatedItems = new();

        foreach (var item in items)
            populatedItems.Add(item.Type == Domain.ShoppingCarts.ProductType.Tour ? GetTour(item) : GetBundle(item));

        return populatedItems;
    }

    private PopulatedItemDto GetTour(Item item)
    {
        try
        {
			var product = _internalTourService.Get((int)item.ProductId).Value;

			return new PopulatedItemDto
			{
				Id = (int)item.Id,
				ShoppingCartId = (int)item.ShoppingCartId,
				Type = (API.Dtos.ShoppingCarts.ProductType)item.Type,
				Product = product,
				ProductName = item.ProductName,
				AdventureCoin = item.AdventureCoin
			};
		}
        catch (Exception)
        {
            throw;
        }
    }

    private PopulatedItemDto GetBundle(Item item)
    {
        try
        {
            var bundle = _tourBundleService.Get((int)item.ProductId);
            var product = new PopulatedTourBundleDto
            {
                Id = (int)bundle.Value.Id,
                Name = bundle.Value.Name,
                Price = bundle.Value.Price,
                Status = bundle.Value.Status,
                AuthorId = bundle.Value.AuthorId
            };

            foreach (var tourId in bundle.Value.TourIds) product.Tours.Add(_internalTourService.Get(tourId).Value);

            return new PopulatedItemDto
            {
                Id = (int)item.Id,
                ShoppingCartId = (int)item.ShoppingCartId,
                Type = (API.Dtos.ShoppingCarts.ProductType)item.Type,
                Product = product,
                ProductName = item.ProductName,
                AdventureCoin = item.AdventureCoin,
            };
        }
        catch (Exception)
        {
            throw;
        }
    }
}
