using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService, IInternalShoppingCartService
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

	public Result<ShoppingCartDto> GetPopulatedByTouristId(int touristId)
	{
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByTouristId(touristId);

            var shoppingCartDto = new ShoppingCartDto
            {
                Id = (int)shoppingCart.Id,
                TouristId = (int)shoppingCart.TouristId,
                Items = shoppingCart.Items.Select(i => _mapper.Map<ItemDto>(i)).ToList()
            };

			foreach (var item in shoppingCartDto.Items)
				item.Product = item.Type == API.Dtos.ShoppingCarts.ProductType.Tour ? GetTour(item.ProductId) : GetBundle(item.ProductId);

			return Result.Ok(shoppingCartDto);
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError($"{e.Message}");
        }
	}

    private TourDetailsDto GetTour(int productId)
    {
        try
        {
			var product = _internalTourService.Get(productId).Value;
            return new TourDetailsDto
            {
                Id = (int)product.Id,
                Name = product.Name,
                Description = product.Description,
                Level = (TourLevel)product.Level,
                Tags = new List<string>(product.Taggs)
            };
		}
        catch (Exception)
        {
            throw;
        }
    }

    private TourBundleDto GetBundle(int bundleId)
    {
        try
        {
            var bundle = _tourBundleService.Get(bundleId);
            if (bundle.IsFailed) throw new KeyNotFoundException("Product ID mismatch.");

            var product = new TourBundleDto
            {
                Id = (int)bundle.Value.Id,
                Name = bundle.Value.Name,
                Price = bundle.Value.Price,
                Status = bundle.Value.Status,
                AuthorId = bundle.Value.AuthorId,
                TourIds = bundle.Value.TourIds,
            };

            foreach (var tourId in product.TourIds) product.Tours!.Add(GetTour(tourId));

            return product;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Result<ShoppingCartDto> Create(int touristId)
    {
        try
        {
            ShoppingCart shoppingCart = new(touristId);
            var result = _shoppingCartRepository.Create(shoppingCart);
            return MapToDto(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
