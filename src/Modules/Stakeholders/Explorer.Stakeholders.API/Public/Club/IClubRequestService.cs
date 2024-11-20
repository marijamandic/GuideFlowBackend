using Explorer.Stakeholders.API.Dtos.Club;
using FluentResults;

namespace Explorer.Stakeholders.API.Public.Club;

public interface IClubRequestService
{
    Result<ClubRequestDto> SubmitMembershipRequest(ClubRequestDto requestDto);
    Result<ClubRequestDto> AcceptMembershipRequest(long requestId);
    Result<ClubRequestDto> DeclineMembershipRequest(long requestId);
    Result<ClubRequestDto> CancelMembershipRequest(long requestId);
    Result<ClubRequestDto> GetRequestStatus(long requestId);
    Result<List<ClubRequestDto>> GetRequestByTouristId(long touristId);

    Result<List<ClubRequestDto>> GetAll();
    public Result<List<ClubRequestDto>> GetRequestByClubId(long clubId);
}
