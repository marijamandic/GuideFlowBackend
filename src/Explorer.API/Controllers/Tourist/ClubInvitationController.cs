using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [HttpGet("all")]
        public ActionResult<IEnumerable<ClubInvitationDto>> GetAll()
        {
            var result = _clubInvitationService.GetAll();
            return CreateResponse(result);
        }

        [HttpGet("club/{clubId:long}")]
        public ActionResult<List<ClubInvitationDto>> GetInvitationsByClub(long clubId)
        {
            var result = _clubInvitationService.GetInvitationsByClub(clubId);
            return CreateResponse(result);
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
            System.Diagnostics.Debug.WriteLine($"In the controller!");

            var result = _clubInvitationService.DeclineInvitation(invitationId);

            if (result.IsFailed)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {string.Join(", ", result.Errors.Select(e => e.Message))}");
                return BadRequest(result.Errors);
            }

            return CreateResponse(result);
        }

        [HttpPut("{invitationId:int}/update")]
        public ActionResult<ClubInvitationDto> UpdateInvitation(int invitationId, [FromBody] ClubInvitationDto invitationDto)
        {
            Debug.WriteLine($"Received PUT request for invitationId: {invitationId}");
            var result = _clubInvitationService.UpdateInvitation(invitationId, invitationDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

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
