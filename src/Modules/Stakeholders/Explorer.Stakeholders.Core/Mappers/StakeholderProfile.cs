using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<ClubInvitationDto, ClubInvitation>().ReverseMap();
        CreateMap<ClubRequestDto, ClubRequest>().ReverseMap();
        CreateMap<ClubMemberDto, ClubMember>().ReverseMap();
        CreateMap<Location, LocationDto>().ReverseMap(); 
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
        CreateMap<ProblemDto, Problem>().ReverseMap();
        CreateMap<RatingAppDto, AppRating>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<ProfileInfoDto, ProfileInfo>().ReverseMap();
    }
}
