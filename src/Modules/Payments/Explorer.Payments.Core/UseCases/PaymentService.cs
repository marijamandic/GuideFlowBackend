using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.Payments;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
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
        private readonly IInternalUserService _internalUserService;
        private readonly ITourService _tourService;



        //private readonly IUserService _userService;
        public PaymentService(IMapper mapper, IInternalUserService userService, IPaymentRepository paymentRepository,IShoppingCartService shoppingCartService,ITourPurchaseTokenService tourPurchaseTokenService, ITourService tourService):base(mapper) 
        { 
            _paymentRepository = paymentRepository;
            _shoppingCartService = shoppingCartService;
            _tourPurchaseTokenService = tourPurchaseTokenService;
            _internalUserService = userService;
            _tourService = tourService;
        }

        public Result<PaymentDto> Create(int touristId)
        {
            try
            {
                var shoppingCartResult = _shoppingCartService.GetByTouristId(touristId);
                if (!shoppingCartResult.IsSuccess)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Shopping cart retrieval failed.");

                var result = _internalUserService.GetTouristById(touristId);
                TouristDto tourist = result.Value;

                int cartSum = shoppingCartResult.Value.Items.Sum(item => item.AdventureCoin);

                if (cartSum > tourist.Wallet)
                {
                    return Result.Fail("Nema novca");
                }

                var paymentDto = new PaymentDto
                {
                    TouristId = touristId,
                    PurchaseDate = DateTime.UtcNow
                };

                var payment = _paymentRepository.Create(MapToDomain(paymentDto));

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
                }

                _internalUserService.TakeTouristAdventureCoins(touristId, cartSum);

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
    }
}
