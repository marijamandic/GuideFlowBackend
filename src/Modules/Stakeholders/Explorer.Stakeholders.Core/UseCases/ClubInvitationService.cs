using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
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
        public ClubInvitationService(IMapper mapper) : base(mapper)
        {

        }
        public Result<ClubInvitationDto> AcceptInvitation(long invitationId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubInvitationDto> CancelInvitation(long invitationId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubInvitationDto> DeclineInvitation(long invitationId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubInvitationDto> GetInvitationStatus(long invitationId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubInvitationDto> SubmitInvitation(ClubInvitationDto invitationDto)
        {
            throw new NotImplementedException();
        }
    }
}
