using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.Club
{
    public class ClubRequestService : BaseService<ClubRequestDto, ClubRequest>, IClubRequestService
    {
        private readonly IClubRequestRepository _clubRequestRepository;
        private readonly IClubMemberService _clubMemberService;

        public ClubRequestService(IMapper mapper, IClubRequestRepository clubRequestRepository, IClubMemberService clubMemberService) : base(mapper)
        {
            _clubRequestRepository = clubRequestRepository;
            _clubMemberService = clubMemberService;
        }

        public Result<List<ClubRequestDto>> GetAll()
        {
            var clubRequests = _clubRequestRepository.GetAll();
            if (clubRequests == null || !clubRequests.Any())
            {
                return Result.Fail<List<ClubRequestDto>>("No requests found.");
            }

            var clubRequestsDtos = clubRequests
                .Select(cr => MapToDto(cr))
                .ToList();

            return Result.Ok(clubRequestsDtos);
        }
        public Result<ClubRequestDto> AcceptMembershipRequest(long requestId)
        {
            var clubRequest = _clubRequestRepository.GetById(requestId);
            if (clubRequest == null)
            {
                return Result.Fail<ClubRequestDto>("Request not found.");
            }

            clubRequest.AcceptRequest();
            _clubRequestRepository.Update(clubRequest);
            _clubMemberService.AddMember(clubRequest.ClubId, clubRequest.TouristId);

            return Result.Ok(MapToDto(clubRequest));
        }


        public Result<ClubRequestDto> CancelMembershipRequest(long requestId)
        {
            var clubRequest = _clubRequestRepository.GetById(requestId);
            if (clubRequest == null)
            {
                return Result.Fail<ClubRequestDto>("Request not found.");
            }

            //otkazivanje zahteva
            clubRequest.CancelRequest();
            _clubRequestRepository.Update(clubRequest);

            return Result.Ok(MapToDto(clubRequest));
        }

        public Result<ClubRequestDto> DeclineMembershipRequest(long requestId)
        {
            var clubRequest = _clubRequestRepository.GetById(requestId);
            if (clubRequest == null)
            {
                return Result.Fail<ClubRequestDto>("Request not found.");
            }

            //odbijanje zahteva
            clubRequest.DeclineRequest();
            _clubRequestRepository.Update(clubRequest);

            return Result.Ok(MapToDto(clubRequest));
        }

        public Result<ClubRequestDto> GetRequestStatus(long requestId)
        {
            var clubRequest = _clubRequestRepository.GetById(requestId);
            if (clubRequest == null)
            {
                return Result.Fail<ClubRequestDto>("Request not found.");
            }

            return Result.Ok(MapToDto(clubRequest));
        }

        public Result<ClubRequestDto> SubmitMembershipRequest(ClubRequestDto requestDto)
        {
            var clubRequest = new ClubRequest(requestDto.TouristId, requestDto.ClubId, requestDto.CreatedAt, requestDto.IsOpened, requestDto.OwnerId, requestDto.ClubName, requestDto.TouristName);
            var createdRequest = _clubRequestRepository.Create(clubRequest);

            return Result.Ok(MapToDto(createdRequest));
        }

        public Result<List<ClubRequestDto>> GetRequestByTouristId(long touristId)
        {
            var clubRequests = _clubRequestRepository.GetByTouristId(touristId);
            if (clubRequests == null)
            {
                return Result.Fail<List<ClubRequestDto>>("Request not found.");
            }
            List<ClubRequestDto> dtos = new();
            foreach (var club in clubRequests)
            {
                ClubRequestDto dto = new()
                {
                    ClubId = club.ClubId,
                    TouristId = club.TouristId,
                    Id = club.Id,
                    Status = (API.Dtos.Club.ClubRequestStatus)Enum.Parse(typeof(API.Dtos.Club.ClubRequestStatus), club.Status.ToString())
                };
                dtos.Add(dto);
            }
            return Result.Ok(dtos);
        }

        public Result<List<ClubRequestDto>> GetRequestByClubId(long clubId)
        {
            var requests = _clubRequestRepository.GetRequestsByClubId(clubId);
            if (requests == null || !requests.Any())
            {
                return Result.Fail<List<ClubRequestDto>>("No club requests found for the given club ID.");
            }

            var mappedRequests = MapToDto(requests);
            return mappedRequests;
        }

        public Result<List<ClubRequestDto>> GetRequestByOwner(long ownerId)
        {
            var requests = _clubRequestRepository.GetRequestsByOwner(ownerId);
            if (requests == null || !requests.Any())
            {
                return Result.Fail<List<ClubRequestDto>>("No club requests found for the given club ID.");
            }

            var mappedRequests = MapToDto(requests);
            return mappedRequests;
        }
    }
}
