using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.Core.Domain.Club;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<ClubInvitationDto, ClubInvitation>().ReverseMap();
        CreateMap<ClubRequestDto, ClubRequest>().ReverseMap();
        CreateMap<ClubMemberDto, ClubMember>().ReverseMap();
    }
}