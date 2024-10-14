using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using AutoMapper;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class RatingProfile : Profile 
    {
        public RatingProfile()
        {
            CreateMap<RatingAppDto, AppRating>().ReverseMap();
        }
    }
}
