using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
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
    public class PurchaseTokenService: BaseService<PurchaseTokenDto,PurchaseToken>, IPurchaseTokenService, IInternalPurchaseTokenService
    {
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public PurchaseTokenService(IMapper mapper,IPurchaseTokenRepository purchaseTokenRepository, IShoppingCartService shoppingCartService) : base(mapper)
        {
            _purchaseTokenRepository = purchaseTokenRepository;
            _shoppingCartService = shoppingCartService;
        }
        public Result<PagedResult<PurchaseTokenDto>> Create(int touristId)
        {
            try
            {
                var shoppingCartResult = _shoppingCartService.GetByTouristId(touristId);
                if (!shoppingCartResult.IsSuccess)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Shopping cart retrieval failed.");

                var purchaseTokens = shoppingCartResult.Value.Items
                    .Select(item => MapToDomain(new PurchaseTokenDto
                    {
                        TouristId = shoppingCartResult.Value.TouristId,
                        Type = item.Type,
                        ProductId = item.ProductId,
                        PurchaseDate = DateTime.UtcNow,
                        AdventureCoin = item.AdventureCoin
                    }))
                    .Select(_purchaseTokenRepository.Create)
                    .ToList();

                _shoppingCartService.ClearCart(touristId);

                return MapToDto(new PagedResult<PurchaseToken>(purchaseTokens, purchaseTokens.Count));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<PagedResult<PurchaseTokenDto>> GetTourTokensByTouristId(int touristId)
        {
            try
            {
                var result = _purchaseTokenRepository.GetTourTokensByTouristId(touristId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
