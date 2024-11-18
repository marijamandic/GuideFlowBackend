using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.PurchaseTokens;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourPurchaseTokenService :BaseService<TourPurchaseTokenDto,TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public TourPurchaseTokenService(IMapper mapper,ITourPurchaseTokenRepository tourPurchaseTokenRepository, IShoppingCartService shoppingCartService) : base(mapper)
        {
            _tourPurchaseTokenRepository = tourPurchaseTokenRepository;
            _shoppingCartService = shoppingCartService;
        }
        public Result<PagedResult<TourPurchaseTokenDto>> Create(int touristId)
        {
            try
            {
                var shoppingCartResult = _shoppingCartService.GetByTouristId(touristId);
                if (!shoppingCartResult.IsSuccess)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Shopping cart retrieval failed.");

                var tourPurchaseTokens = shoppingCartResult.Value.SingleItems
                    .Select(singleItem => MapToDomain(new TourPurchaseTokenDto
                    {
                        TouristId = shoppingCartResult.Value.TouristId,
                        TourId = singleItem.TourId,
                        PurchaseDate = DateTime.Now,
                        Price = singleItem.Price
                    }))
                    .Select(_tourPurchaseTokenRepository.Create)
                    .ToList();

                return MapToDto(new PagedResult<TourPurchaseToken>(tourPurchaseTokens, tourPurchaseTokens.Count));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        public Result<PagedResult<TourPurchaseTokenDto>> GetAllByTouristId(int touristId)
        {
            try
            {
                var result = _tourPurchaseTokenRepository.GetAllByTouristId(touristId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourPurchaseTokenDto> GetByTouristAndTourId(int touristId, int tourId)
        {
            try
            {
                var result = _tourPurchaseTokenRepository.GetByTourAndTouristId(touristId, tourId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
