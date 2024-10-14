using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class TourSpecificationsProfile : Profile
    {
        public TourSpecificationsProfile()
        {
            CreateMap<TourSpecificationDto, TourSpecifications>().ReverseMap();
        }
    }
}
