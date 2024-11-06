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
        CreateMap<RatingAppDto, AppRating>().ReverseMap();
        CreateMap<ProfileInfoDto, ProfileInfo>().ReverseMap();

        CreateMap<ProblemDto, Problem>().IncludeAllDerived()
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src =>
                new Details((Domain.Problems.ProblemCategory)(int)src.Details.Category, (Domain.Problems.ProblemPriority)(int)src.Details.Priority, src.Details.Description)))
            .ForMember(dest => dest.Resolution, opt => opt.MapFrom(src => new Resolution(src.Resolution.ReportedAt, src.Resolution.IsResolved, src.Resolution.Deadline)))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages.Select(m => new Message(m.ProblemId, m.UserId, m.Content, m.PostedAt))));
        CreateMap<Problem, ProblemDto>().IncludeAllDerived()
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src =>
                new DetailsDto {
                    Category = (API.Dtos.Problems.ProblemCategory)(int)src.Details.Category,
                    Priority = (API.Dtos.Problems.ProblemPriority)(int)src.Details.Priority,
                    Description = src.Details.Description
                }
            ))
            .ForMember(dest => dest.Resolution, opt => opt.MapFrom(src => new ResolutionDto { ReportedAt = src.Resolution.ReportedAt, IsResolved = src.Resolution.IsResolved, Deadline = src.Resolution.Deadline }))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages.Select(m =>
                new MessageDto { Id = (int)m.Id, ProblemId = (int)m.ProblemId, UserId = (int)m.UserId, Content = m.Content, PostedAt = m.PostedAt })));

        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<ProblemNotificationDto, ProblemNotification>().ReverseMap();
    }
}
