﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System.Linq;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Shopping;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<PriceDto, Price>().ReverseMap();
        CreateMap<TransportDurationDto, TransportDuration>().ReverseMap();
        
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourSpecificationDto, TourSpecifications>().ReverseMap();
        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap(); 
        CreateMap<TourObjectDto, TourObject>().ReverseMap();
        CreateMap<EquipmentManagementDto, EquipmentManagement>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<TourExecutionDto,TourExecution>().ReverseMap();
        CreateMap<PublicPointDto, PublicPoint>().ReverseMap();
        CreateMap<CheckPointStatusDto, CheckpointStatus>().ForMember(cs => cs.Checkpoint, opt => opt.MapFrom(src => src.Checkpoint)).ReverseMap();
        CreateMap<CheckPointStatusDto, CheckpointStatus>().ReverseMap();    
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<PurchaseTokenDto, PurchaseToken>().ReverseMap();
    }
}