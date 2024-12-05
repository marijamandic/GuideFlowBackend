using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Payments;
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
        private readonly ITourBundleService _tourBundleService;

        public TourPurchaseTokenService(IMapper mapper,ITourPurchaseTokenRepository purchaseTokenRepository, ITourBundleService tourBundleService) : base(mapper)
        {
            _purchaseTokenRepository = purchaseTokenRepository;
            _tourBundleService = tourBundleService;
        }
        public Result<PagedResult<TourPurchaseTokenDto>> Create(PaymentDto payment)
        {
            try
            {
                var tourProducts = payment.PaymentItems.FindAll(pi => pi.Type == ProductType.Tour);
                var bundleProducts = payment.PaymentItems.FindAll(pi => pi.Type == ProductType.Bundle);
                var purchaseTokens = new List<TourPurchaseToken>();
                if(tourProducts.Any())
                    purchaseTokens.AddRange(ProcessTourProducts(tourProducts, payment.TouristId));
                if(bundleProducts.Any())
                    purchaseTokens.AddRange(ProcessBundleProducts(bundleProducts, payment.TouristId));
                return MapToDto(new PagedResult<TourPurchaseToken>(purchaseTokens, purchaseTokens.Count));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private List<TourPurchaseToken> ProcessTourProducts(List<PaymentItemDto> tourProducts, int touristId)
        {
            return tourProducts
                .Select(tourProduct =>
                    _purchaseTokenRepository.Create(MapToDomain(new TourPurchaseTokenDto
                    {
                        TouristId = touristId,
                        TourId = tourProduct.ProductId
                    })))
                .ToList();
        }


        public List<TourPurchaseToken> ProcessBundleProducts(List<PaymentItemDto> bundleProducts, int touristId)
        {
            return bundleProducts
                .SelectMany(bundleProduct =>
                {
                    var bundleResult = _tourBundleService.Get(bundleProduct.ProductId);
                    if (bundleResult.IsFailed || bundleResult.Value == null)
                    {
                        throw new ArgumentException("Invalid bundle data for ProductId: " + bundleProduct.ProductId);
                    }

                    return bundleResult.Value.TourIds.Select(tourId =>
                        _purchaseTokenRepository.Create(MapToDomain(new TourPurchaseTokenDto
                        {
                            TouristId = touristId,
                            TourId = tourId
                        })));
                })
                .ToList();
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
