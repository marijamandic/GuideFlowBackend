using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Public;
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
    public class PaymentService : BaseService<PaymentDto, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;
        public PaymentService(IMapper mapper,IPaymentRepository paymentRepository,IShoppingCartService shoppingCartService,ITourPurchaseTokenService tourPurchaseTokenService):base(mapper) 
        { 
            _paymentRepository = paymentRepository;
            _shoppingCartService = shoppingCartService;
            _tourPurchaseTokenService = tourPurchaseTokenService;
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
