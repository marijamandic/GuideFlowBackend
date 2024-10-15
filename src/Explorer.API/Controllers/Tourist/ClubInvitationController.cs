using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/invitation/clubInvitation")]
    public class ClubInvitationController : BaseApiController
    {
        private readonly IClubInvitationService _clubInvitationService;

        public ClubInvitationController(IClubInvitationService clubInvitationService)
        {
            _clubInvitationService = clubInvitationService;
        }

        [HttpGet("{invitationId:int}")]
        public ActionResult<ClubInvitationDto> GetInvitation(int invitationId)
        {
            var result = _clubInvitationService.GetInvitationStatus(invitationId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubInvitationDto> Create([FromBody] ClubInvitationDto invitationDto)
        {
            var result = _clubInvitationService.SubmitInvitation(invitationDto);
            return CreateResponse(result);
        }

        [HttpPut("{invitationId:int}/accept")]
        public ActionResult<ClubInvitationDto> Accept(int invitationId)
        {
            var result = _clubInvitationService.AcceptInvitation(invitationId);
            return CreateResponse(result);
        }

        [HttpPut("{invitationId:int}/decline")]
        public ActionResult<ClubInvitationDto> Decline(int invitationId)
        {
            var result = _clubInvitationService.DeclineInvitation(invitationId);
            return CreateResponse(result);
        }

        [HttpDelete("{invitationId:int}")]
        public ActionResult Cancel(int invitationId)
        {
            var result = _clubInvitationService.CancelInvitation(invitationId);
            return CreateResponse(result);
        }
    }
}
