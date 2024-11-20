using AutoMapper;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<ShoppingCartDto, ShoppingCart>().IncludeAllDerived()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(i =>
                new Item(i.Id, i.ShoppingCartId, (Domain.ShoppingCarts.ProductType)i.Type, i.ProductId, i.ProductName, i.AdventureCoin))));

        CreateMap<ShoppingCart, ShoppingCartDto>().IncludeAllDerived()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(i => 
                new ItemDto { Id = (int)i.Id, ShoppingCartId = (int)i.ShoppingCartId, Type = (API.Dtos.ShoppingCarts.ProductType)i.Type,
                    ProductId = (int)i.ProductId, ProductName = i.ProductName, AdventureCoin = i.AdventureCoin })));

        CreateMap<ItemDto, Item>().ReverseMap();
        CreateMap<PurchaseTokenDto, PurchaseToken>().ReverseMap();
    }
}
