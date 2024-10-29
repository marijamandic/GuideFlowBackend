using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers
{
    public class TourSpecificationsProfile : Profile
    {
        public TourSpecificationsProfile()
        {
            CreateMap<TourSpecificationDto, TourSpecifications>().ReverseMap();
        }
    }
}
