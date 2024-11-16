using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.Club
{
    public class ClubInvitationService : BaseService<ClubInvitationDto, ClubInvitation>, IClubInvitationService
    {
        private readonly IClubInvitationRepository _clubInvitationRepository;
        private readonly IClubMemberService _clubMemberService;


        public ClubInvitationService(IMapper mapper, IClubInvitationRepository clubInvitationRepository, IClubMemberService clubMemberService) : base(mapper)
        {
            _clubInvitationRepository = clubInvitationRepository;
            _clubMemberService = clubMemberService;
        }

        public Result<ClubInvitationDto> AcceptInvitation(long invitationId)
        {
            var clubInvitation = _clubInvitationRepository.GetById(invitationId);
            if (clubInvitation == null)
            {
                return Result.Fail<ClubInvitationDto>("Invitation not found.");
            }

            clubInvitation.AcceptInvitation();
            _clubInvitationRepository.Update(clubInvitation);

            _clubMemberService.AddMember(clubInvitation.ClubId, clubInvitation.TouristID);

            return Result.Ok(MapToDto(clubInvitation));
        }

        public Result<List<ClubInvitationDto>> GetInvitationsByClub(long clubId)
        {
            var clubInvitations = _clubInvitationRepository.GetByClubId(clubId);
            if (clubInvitations == null || !clubInvitations.Any())
            {
                return Result.Fail<List<ClubInvitationDto>>("No invitations found for this club.");
            }

            var clubInvitationDtos = clubInvitations
                .Select(ci => MapToDto(ci))
                .ToList();

            return Result.Ok(clubInvitationDtos);
        }


        public Result<ClubInvitationDto> CancelInvitation(long invitationId)
        {
            var clubInvitation = _clubInvitationRepository.GetById(invitationId);
            if (clubInvitation == null)
            {
                return Result.Fail<ClubInvitationDto>("Invitation not found.");
            }
            clubInvitation.CancelInvitation();
            _clubInvitationRepository.Update(clubInvitation);
            return Result.Ok(MapToDto(clubInvitation));
        }

        public Result<ClubInvitationDto> DeclineInvitation(long invitationId)
        {
            System.Diagnostics.Debug.WriteLine($"In the service with ID: {invitationId}");

            try
            {
                var clubInvitation = _clubInvitationRepository.GetById(invitationId);
                clubInvitation.DeclineInvitation();
                _clubInvitationRepository.Update(clubInvitation);
                return Result.Ok(MapToDto(clubInvitation));
            }
            catch (KeyNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                return Result.Fail<ClubInvitationDto>(ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
                return Result.Fail<ClubInvitationDto>("An unexpected error occurred.");
            }
        }

        private Domain.Club.ClubInvitationStatus MapStatus(API.Dtos.Club.ClubInvitationStatus dtoStatus)
        {
            return dtoStatus switch
            {
                API.Dtos.Club.ClubInvitationStatus.PENDING => Domain.Club.ClubInvitationStatus.PENDING,
                API.Dtos.Club.ClubInvitationStatus.ACCEPTED => Domain.Club.ClubInvitationStatus.ACCEPTED,
                API.Dtos.Club.ClubInvitationStatus.DECLINED => Domain.Club.ClubInvitationStatus.DECLINED,
                API.Dtos.Club.ClubInvitationStatus.CANCELLED => Domain.Club.ClubInvitationStatus.CANCELLED,
                _ => throw new ArgumentOutOfRangeException(nameof(dtoStatus), $"Unsupported status value: {dtoStatus}")
            };
        }

        public Result<ClubInvitationDto> UpdateInvitation(int invitationId, ClubInvitationDto invitationDto)
        {
            var clubInvitation = _clubInvitationRepository.GetById(invitationId);
            if (clubInvitation == null)
            {
                return Result.Fail<ClubInvitationDto>("Invitation not found.");
            }

            var domainStatus = MapStatus(invitationDto.Status);
            clubInvitation.UpdateDetails(invitationDto.ClubId, invitationDto.TouristId, domainStatus);
            _clubInvitationRepository.Update(clubInvitation);
            return Result.Ok(MapToDto(clubInvitation));
        }

        public Result<ClubInvitationDto> GetInvitationStatus(long invitationId)
        {
            var clubInvitation = _clubInvitationRepository.GetById(invitationId);
            if (clubInvitation == null)
            {
                return Result.Fail<ClubInvitationDto>("Invitation not found.");
            }
            return Result.Ok(MapToDto(clubInvitation));
        }

        public Result<ClubInvitationDto> SubmitInvitation(ClubInvitationDto invitationDto)
        {
            var clubInvitation = new ClubInvitation(invitationDto.ClubId, invitationDto.TouristId, Domain.Club.ClubInvitationStatus.PENDING);    // jer imam enum u dto-u
            var createdInvitation = _clubInvitationRepository.Create(clubInvitation);
            return Result.Ok(MapToDto(createdInvitation));
        }

        public Result<List<ClubInvitationDto>> GetAll()
        {
            var clubInvitations = _clubInvitationRepository.GetAll();
            if (clubInvitations == null || !clubInvitations.Any())
            {
                return Result.Fail<List<ClubInvitationDto>>("No invitations found.");
            }

            var clubInvitationDtos = clubInvitations
                .Select(ci => MapToDto(ci))
                .ToList();

            return Result.Ok(clubInvitationDtos);
        }
    }
}
