using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.API.Dtos;
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
            var imageUrl = GetImageUrl(itemInput.Type, itemInput.ProductId);

            var item = new ItemDto
            {
                ShoppingCartId = (int)shoppingCart.Id,
                Type = itemInput.Type,
                ProductId = itemInput.ProductId,
                ProductName = itemInput.ProductName,
                ImageUrl = imageUrl,
                AdventureCoin = itemInput.AdventureCoin
            };

            shoppingCart.AddToCart(_mapper.Map<Item>(item));
            _shoppingCartRepository.Save(shoppingCart);

            var items = shoppingCart.Items.Select(i => _mapper.Map<ItemDto>(i)).ToList();
            return new PagedResult<ItemDto>(items, items.Count);
        }
        catch(KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

	private string GetImageUrl(API.Dtos.ShoppingCarts.ProductType type, int productId)
	{
        TourDto tour = type == API.Dtos.ShoppingCarts.ProductType.Tour ?
            GetTour(productId) :
            GetFirstTourFromBundle(productId);

        return tour.Checkpoints[0].ImageUrl!;
	}

    private TourDto GetTour(int id)
    {
        var result = _internalTourService.Get(id);
        if (result.IsFailed) throw new KeyNotFoundException("Tour ID mismatch");
        return result.Value;
    }

    private TourBundleDto GetBundle(int id)
    {
        var result = _tourBundleService.Get(id);
        if (result.IsFailed) throw new KeyNotFoundException("Bundle ID mismatch");
        return result.Value;
    }

    private TourDto GetFirstTourFromBundle(int bundleId)
    {
        var bundle = GetBundle(bundleId);
        return GetTour(bundle.TourIds[0]);
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
				item.Product = item.Type == API.Dtos.ShoppingCarts.ProductType.Tour ? GetTourDetails(item.ProductId) : GetPopulatedBundle(item.ProductId);

			return Result.Ok(shoppingCartDto);
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError($"{e.Message}");
        }
	}

    private TourDetailsDto GetTourDetails(int productId)
    {
        TourDto tour;

        try
        {
            tour = GetTour(productId);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }

        return new TourDetailsDto
        {
            Id = tour.Id,
            Name = tour.Name,
            Description = tour.Description,
            ImageUrl = tour.Checkpoints[0].ImageUrl!,
            Level = (TourLevel)tour.Level,
            Tags = new List<string>(tour.Taggs)
        };
    }

    private TourBundleDto GetPopulatedBundle(int bundleId)
    {
        TourBundleDto bundle;

        try
        {
            bundle = GetBundle(bundleId);
            foreach (var tourId in bundle.TourIds) bundle.Tours.Add(GetTourDetails(tourId));
        }
        catch (Exception)
        {
            throw;
        }

        return bundle;
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
