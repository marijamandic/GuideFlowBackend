using AutoMapper;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.API.Dtos.Problems;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<ClubInvitationDto, ClubInvitation>().ReverseMap();
        CreateMap<ClubRequestDto, ClubRequest>().ReverseMap();
        CreateMap<ClubMemberDto, ClubMember>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<ProblemDto, Problem>().ReverseMap();
        CreateMap<RatingAppDto, AppRating>().ReverseMap();
        CreateMap<ResolutionDto, Resolution>().ReverseMap();
        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<ProfileInfoDto, ProfileInfo>().ReverseMap();
    }
}