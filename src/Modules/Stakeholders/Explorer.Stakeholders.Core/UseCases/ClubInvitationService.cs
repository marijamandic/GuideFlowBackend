using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubInvitationService : BaseService<ClubInvitationDto, ClubInvitation>, IClubInvitationService
    {
        private readonly IClubInvitationRepository _clubInvitationRepository;

        public ClubInvitationService(IMapper mapper, IClubInvitationRepository clubInvitationRepository)
            : base(mapper)
        {
            _clubInvitationRepository = clubInvitationRepository;
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
            return Result.Ok(MapToDto(clubInvitation));
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

        private Explorer.Stakeholders.Core.Domain.ClubInvitationStatus MapStatus(Explorer.Stakeholders.API.Dtos.ClubInvitationStatus dtoStatus)
        {
            return dtoStatus switch
            {
                Explorer.Stakeholders.API.Dtos.ClubInvitationStatus.PENDING => Explorer.Stakeholders.Core.Domain.ClubInvitationStatus.PENDING,
                Explorer.Stakeholders.API.Dtos.ClubInvitationStatus.ACCEPTED => Explorer.Stakeholders.Core.Domain.ClubInvitationStatus.ACCEPTED,
                Explorer.Stakeholders.API.Dtos.ClubInvitationStatus.DECLINED => Explorer.Stakeholders.Core.Domain.ClubInvitationStatus.DECLINED,
                Explorer.Stakeholders.API.Dtos.ClubInvitationStatus.CANCELLED => Explorer.Stakeholders.Core.Domain.ClubInvitationStatus.CANCELLED,
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
            var clubInvitation = new ClubInvitation(invitationDto.ClubId, invitationDto.TouristId, Domain.ClubInvitationStatus.PENDING);    // jer imam enum u dto-u
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
