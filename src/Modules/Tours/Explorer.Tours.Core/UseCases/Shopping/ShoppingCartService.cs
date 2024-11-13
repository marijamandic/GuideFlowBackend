using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper mapper;
        
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper) : base(mapper)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
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

        public Result<ShoppingCartDto> Create(ShoppingCartDto entity)
        {
            try
            {
                var result = _shoppingCartRepository.Create(MapToDomain(entity));
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

        public Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId)
        {

            ShoppingCart shoppingCart = _shoppingCartRepository.Get(touristId);
            if(shoppingCart == null)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.NotFound).WithError("Shopping cart not founr");
            }
            shoppingCart.AddItemToCart(mapper.Map<OrderItem>(orderItemDto));
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

    }
}
