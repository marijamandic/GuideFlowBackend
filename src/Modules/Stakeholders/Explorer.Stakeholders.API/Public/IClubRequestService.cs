using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubRequestService
{
    Result<ClubRequestDto> SubmitMembershipRequest(ClubRequestDto requestDto);
    Result<ClubRequestDto> AcceptMembershipRequest(int requestId);
    Result<ClubRequestDto> DeclineMembershipRequest(int requestId);
    Result<ClubRequestDto> CancelMembershipRequest(int requestId);
    Result<ClubRequestDto> GetRequestStatus(int requestId);

}
