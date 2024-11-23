using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourPurchaseTokenService: BaseService<TourPurchaseTokenDto,TourPurchaseToken>, ITourPurchaseTokenService, IInternalPurchaseTokenService
    {
        private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;

        public TourPurchaseTokenService(IMapper mapper,ITourPurchaseTokenRepository purchaseTokenRepository) : base(mapper)
        {
            _purchaseTokenRepository = purchaseTokenRepository;
        }
        public Result<PagedResult<TourPurchaseTokenDto>> Create(PaymentDto payment) //za sad ovako dok nema bundle u payment, sve su sad ture
        {
            try
            {
                var purchaseTokens = payment.PaymentItems
                    .Select(item => MapToDomain(new TourPurchaseTokenDto
                    {
                        TouristId = payment.TouristId,
                        TourId = item.ProductId
                    }))
                    .Select(_purchaseTokenRepository.Create)
                    .ToList();

                return MapToDto(new PagedResult<TourPurchaseToken>(purchaseTokens, purchaseTokens.Count));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<PagedResult<TourPurchaseTokenDto>> GetTokensByTouristId(int touristId)
        {
            try
            {
                var result = _purchaseTokenRepository.GetTokensByTouristId(touristId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourPurchaseTokenDto> GetTokenByTouristAndTourId(int touristId, int tourId)
        {
            try
            {
                var result = _purchaseTokenRepository.GetTokenByTouristAndTourId(touristId,tourId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
