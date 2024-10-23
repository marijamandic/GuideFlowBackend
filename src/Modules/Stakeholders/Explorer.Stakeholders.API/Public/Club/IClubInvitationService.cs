using Explorer.Stakeholders.API.Dtos.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public.Club
{
    public interface IClubInvitationService
    {
        Result<ClubInvitationDto> SubmitInvitation(ClubInvitationDto invitationDto);
        Result<ClubInvitationDto> AcceptInvitation(long invitationId);
        Result<ClubInvitationDto> DeclineInvitation(long invitationId);
        Result<ClubInvitationDto> CancelInvitation(long invitationId);
        Result<ClubInvitationDto> GetInvitationStatus(long invitationId);
        Result<List<ClubInvitationDto>> GetAll();
        Result<ClubInvitationDto> UpdateInvitation(int invitationId, ClubInvitationDto invitationDto);
    }

}
