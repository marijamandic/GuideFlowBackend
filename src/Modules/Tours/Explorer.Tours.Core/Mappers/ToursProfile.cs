using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<TourSpecificationDto, TourSpecifications>().ReverseMap();
        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap(); 
        CreateMap<TourObjectDto, TourObject>().ReverseMap();
        CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
        CreateMap<EquipmentManagementDto, EquipmentManagement>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<TourExecutionDto,TourExecution>().ReverseMap();
        CreateMap<CheckPointStatusDto, CheckpointStatus>().ReverseMap();    
    }
}