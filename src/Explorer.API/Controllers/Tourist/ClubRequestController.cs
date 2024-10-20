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

        [HttpGet("{requestId:long}")]
        public ActionResult<ClubRequestDto> GetRequest(long requestId)
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

        [HttpPut("{requestId:long}/accept")]
        public ActionResult<ClubRequestDto> Accept(long requestId)
        {
            var result = _clubRequestService.AcceptMembershipRequest(requestId);
            return CreateResponse(result);
        }

        [HttpPut("{requestId:long}/decline")]
        public ActionResult<ClubRequestDto> Decline(long requestId)
        {
            var result = _clubRequestService.DeclineMembershipRequest(requestId);
            return CreateResponse(result);
        }

        [HttpDelete("{requestId:long}")]
        public ActionResult Cancel(long requestId)
        {
            var result = _clubRequestService.CancelMembershipRequest(requestId);
            return CreateResponse(result);
        }


    }
}
