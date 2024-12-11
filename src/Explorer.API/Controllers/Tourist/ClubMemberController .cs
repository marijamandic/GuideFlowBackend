using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/members/clubMember")]
    public class ClubMemberController : BaseApiController
    {
        private readonly IClubMemberService _clubMemberService;

        public ClubMemberController(IClubMemberService clubMemberService)
        {
            _clubMemberService = clubMemberService;
        }

        [HttpGet("{clubId:long}/all")]
        public ActionResult<IEnumerable<ClubMemberDto>> GetAllMembers(long clubId)
        {
            var result = _clubMemberService.GetMembersByClub(clubId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubMemberDto> AddMember([FromBody] ClubMemberDto memberDto)
        {
            var result = _clubMemberService.AddMember(memberDto.ClubId, memberDto.UserId);
            return CreateResponse(result);
        }

        [HttpDelete("{clubId:long}/{userId:long}")]
        public ActionResult RemoveMember(long clubId, long userId)
        {
            var result = _clubMemberService.RemoveMember(clubId, userId);
            return CreateResponse(result);
        }

        [HttpGet("{userId:long}/allMembers")]

        public ActionResult<IEnumerable<ClubMemberDto>> GetAllByUserId(long userId)
        {
            var result = _clubMemberService.GetMembersByUserId(userId);
            return CreateResponse(result);
        }
    }
}
