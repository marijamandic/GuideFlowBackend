using AutoMapper;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain.PurchaseTokens;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<ShoppingCartDto, ShoppingCart>().IncludeAllDerived()
            .ForMember(dest => dest.SingleItems, opt => opt.MapFrom(src => src.SingleItems.Select(i => new SingleItem(i.Id, i.ShoppingCartId, i.TourId, i.TourName, i.AdventureCoin))));

        CreateMap<ShoppingCart, ShoppingCartDto>().IncludeAllDerived()
            .ForMember(dest => dest.SingleItems, opt => opt.MapFrom(src => src.SingleItems.Select(i => 
                new SingleItemDto { Id = (int)i.Id, ShoppingCartId = (int)i.ShoppingCartId, TourId = (int)i.TourId, TourName = i.TourName, AdventureCoin = i.AdventureCoin })));

        CreateMap<SingleItemDto, SingleItem>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
    }
}
