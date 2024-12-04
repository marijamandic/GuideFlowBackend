using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.Payments;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class PaymentService : BaseService<PaymentDto, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;
        private readonly IUserService _userService;
        public PaymentService(IMapper mapper, IUserService userService, IPaymentRepository paymentRepository,IShoppingCartService shoppingCartService,ITourPurchaseTokenService tourPurchaseTokenService):base(mapper) 
        { 
            _paymentRepository = paymentRepository;
            _shoppingCartService = shoppingCartService;
            _tourPurchaseTokenService = tourPurchaseTokenService;
            _userService = userService;
        }

        public Result<PaymentDto> Create(int touristId)
        {
            try
            {
                var shoppingCartResult = _shoppingCartService.GetByTouristId(touristId);
                if (!shoppingCartResult.IsSuccess)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Shopping cart retrieval failed.");

                var paymentDto = new PaymentDto
                {
                    TouristId = touristId,
                    PurchaseDate = DateTime.UtcNow
                };

                var payment = _paymentRepository.Create(MapToDomain(paymentDto));

                var shoppingCartCostSum = 0;

                foreach (var item in shoppingCartResult.Value.Items)
                {
                    var paymentItem = _mapper.Map<PaymentItem>(new PaymentItemDto
                    {
                        PaymentId = (int)payment.Id,
                        ProductId = item.ProductId,
                        Type = item.Type,
                        ProductName = item.ProductName,
                        AdventureCoin = item.AdventureCoin
                    });
                    payment.AddToPayment(paymentItem);
                    shoppingCartCostSum += item.AdventureCoin;
                }

                _userService.TakeTouristAdventureCoins(touristId, shoppingCartCostSum);

                _shoppingCartService.ClearCart(touristId);

                _paymentRepository.Save(payment);

                _tourPurchaseTokenService.Create(MapToDto(payment));

                return MapToDto(payment);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<PagedResult<PaymentDto>> GetAllByTouristId(int touristId)
        {
            try
            {
                var result = _paymentRepository.GetAllByTouristId(touristId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result Checkout(int touristId)
        {
            var result = _userService.GetTouristById(touristId);
            TouristDto tourist = result.Value;
            //ShoppingCart touristsCart = _shoppingCartRepository.GetByTouristId(touristId);
            var touristsCart = _shoppingCartService.GetByTouristId(touristId);

            int shoppingCartSum = 0;

            foreach (var item in touristsCart.Value.Items)
            {
                shoppingCartSum += item.AdventureCoin;
            }

            if (shoppingCartSum <= tourist.Wallet)
            {
                Create(touristId);
                return Result.Ok();
            }
            return Result.Fail("Nema novaca");
        }
    }
}
