using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/request/clubRequest")]
    public class ClubRequestController : BaseApiController
    {
        private readonly IClubRequestService _clubRequestService;

        public ClubRequestController(IClubRequestService clubRequestService)
        {
            _clubRequestService = clubRequestService;
        }

        [HttpGet("{requestId:int}")]
        public ActionResult<ClubRequestDto> GetRequest(int requestId)
        {
            var result = _clubRequestService.GetRequestStatus(requestId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubRequestDto> Create([FromBody] ClubRequestDto requestDto)
        {
            var result = _clubRequestService.SubmitMembershipRequest(requestDto);
            return CreateResponse(result);
        }

        [HttpPut("{requestId:int}/accept")]
        public ActionResult<ClubRequestDto> Accept(int requestId)
        {
            var result = _clubRequestService.AcceptMembershipRequest(requestId);
            return CreateResponse(result);
        }

        [HttpPut("{requestId:int}/decline")]
        public ActionResult<ClubRequestDto> Decline(int requestId)
        {
            var result = _clubRequestService.DeclineMembershipRequest(requestId);
            return CreateResponse(result);
        }

        [HttpDelete("{requestId:int}")]
        public ActionResult Cancel(int requestId)
        {
            var result = _clubRequestService.CancelMembershipRequest(requestId);
            return CreateResponse(result);
        }


    }
}
