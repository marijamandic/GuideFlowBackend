using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain.PurchaseTokens;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile() 
    {
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
    }
}
