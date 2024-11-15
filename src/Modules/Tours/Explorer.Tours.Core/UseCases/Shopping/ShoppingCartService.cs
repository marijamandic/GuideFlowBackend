using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Shopping;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public.Shopping;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService, IInternalShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper mapper;
        private readonly IPurchaseTokensService _purchaseTokenService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper, IPurchaseTokensService purchaseTokenService) : base(mapper)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
            _purchaseTokenService = purchaseTokenService;
        }

        public Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize)
        {
            var result = _shoppingCartRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<ShoppingCartDto> Get(int id)
        {
            try
            {
                var result = _shoppingCartRepository.Get(id);
                return MapToDto(result);
            }
            catch(KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public Result<ShoppingCartDto> Create(long userId)
        {
            var shoppingCart = new ShoppingCartDto
            {
                TouristId = userId,
            };
            try
            {
                var result = _shoppingCartRepository.Create(MapToDomain(shoppingCart));
                return MapToDto(result);

            }
            catch(ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> Update(ShoppingCartDto entity)
        {
            try
            {
                var result = _shoppingCartRepository.Update(MapToDomain(entity));
                return MapToDto(result);
            }
            catch(KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch(ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                _shoppingCartRepository.Delete(id);
                return Result.Ok();
            }
            catch(KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> AddItemToCart(long touristId, OrderItemDto orderItemDto)
        {

            ShoppingCart shoppingCart = _shoppingCartRepository.Get(touristId);
            if(shoppingCart == null)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.NotFound).WithError("Shopping cart not founr");
            }
            var orderItem = new OrderItem(orderItemDto.TourID, orderItemDto.TourName,orderItemDto.Price);
            shoppingCart.AddItemToCart(orderItem);
            var result = _shoppingCartRepository.Update(shoppingCart);
            return MapToDto(result);
        }

        public Result<ShoppingCartDto> RemoveItemFromCart(long touristId, long tourId)
        {
            ShoppingCart shoppingCart = _shoppingCartRepository.Get(touristId);
            if (shoppingCart == null)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.NotFound).WithError("Shopping cart not founr");
            }
            shoppingCart.RemoveItemFromCart(tourId);
            var result = _shoppingCartRepository.Update(shoppingCart);
            return MapToDto(result);
        }

        public Result<ShoppingCartDto> GetByUserId(long touristId)
        {
            var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);
            if (shoppingCart == null)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.NotFound).WithError("Shopping cart not found for the specified user.");
            }

            // Mapiranje domen entiteta u DTO i vraćanje rezultata
            var shoppingCartDto = mapper.Map<ShoppingCartDto>(shoppingCart);
            return Result.Ok(shoppingCartDto);
        }

        public Result<ShoppingCartDto> ClearCart(long touristId)
        {
            ShoppingCart shoppingCart = _shoppingCartRepository.Get(touristId);
            if (shoppingCart == null)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.NotFound).WithError("Shopping cart not founr");
            }
            foreach ( var item in shoppingCart.Items)
            {
                PurchaseTokenDto token = new PurchaseTokenDto();
                token.UserId = (int)shoppingCart.TouristId;
                token.TourId = (int)item.TourID;
                _purchaseTokenService.Create(token);
            }
            shoppingCart.ClearCart();
            var result = _shoppingCartRepository.Update(shoppingCart);
            return MapToDto(result);
        }

    }
}
