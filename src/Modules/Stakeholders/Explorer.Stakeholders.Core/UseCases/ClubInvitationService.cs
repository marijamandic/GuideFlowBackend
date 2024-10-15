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
            var clubInvitation = _clubInvitationRepository.GetById(invitationId);
            if (clubInvitation == null)
            {
                return Result.Fail<ClubInvitationDto>("Invitation not found.");
            }
            clubInvitation.DeclineInvitation();
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
            var clubInvitation = new ClubInvitation(invitationDto.ClubId, invitationDto.TouristID, Domain.ClubInvitationStatus.PENDING);    // jer imam enum u dto-u
            var createdInvitation = _clubInvitationRepository.Create(clubInvitation);
            return Result.Ok(MapToDto(createdInvitation));
        }
    }
}
