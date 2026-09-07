using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
﻿using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
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

        [HttpGet("getAllRequests")]
        public ActionResult<IEnumerable<ClubInvitationDto>> GetAll()
        {
            var result = _clubRequestService.GetAll();
            return CreateResponse(result);
        }

        [HttpGet("{requestId:long}")]
        public ActionResult<ClubRequestDto> GetRequest(long requestId)
        {
            var result = _clubRequestService.GetRequestStatus(requestId);
            return CreateResponse(result);
        }

        [HttpGet("club/{clubId:long}")]
        public ActionResult<List<ClubRequestDto>> GetRequestByClubId(long clubId)
        {
            var result = _clubRequestService.GetRequestByClubId(clubId);
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

        [HttpPut("{requestId:long}/cancel")]
        public ActionResult<ClubRequestDto> Cancel(long requestId)
        {
            var result = _clubRequestService.CancelMembershipRequest(requestId);
            return CreateResponse(result);
        }

        /*[HttpDelete("{requestId:long}")]
        public ActionResult Cancel(long requestId)
        {
            var result = _clubRequestService.CancelMembershipRequest(requestId);
            return CreateResponse(result);
        }
        */

        [HttpDelete("{id}")]
        public ActionResult DeleteMembershipRequest(long id)
        {
            var result = _clubRequestService.DeleteMembershipRequest(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }

            return NotFound(result.Errors.First().Message);
        }


        [HttpGet("for-tourist/{touristId:long}")]
        public ActionResult<IEnumerable<ClubRequestDto>> GetRequestByTouristId(long touristId)
        {
            var result = _clubRequestService.GetRequestByTouristId(touristId);
            return CreateResponse(result);
        }

        [HttpGet("for-owner/{ownerId:long}")]
        public ActionResult<IEnumerable<ClubRequestDto>> GetRequestByOwner(long ownerId)
        {
            var result = _clubRequestService.GetRequestByOwner(ownerId);
            return CreateResponse(result);
        }

    }
}
