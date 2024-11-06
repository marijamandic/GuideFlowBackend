﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Shopping;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();

        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap(); 
        CreateMap<TourObjectDto, TourObject>().ReverseMap();
        CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
        CreateMap<EquipmentManagementDto, EquipmentManagement>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<PurchaseTokenDto, PurchaseToken>().ReverseMap();
    }
}