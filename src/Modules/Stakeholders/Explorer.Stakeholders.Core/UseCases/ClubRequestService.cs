using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubRequestService : BaseService<ClubRequestDto, ClubRequest>, IClubRequestService
    {
        private readonly IClubRequestRepository _clubRequestRepository;
        public ClubRequestService(IMapper mapper, IClubRequestRepository clubRequestRepository) : base(mapper)
        {
            _clubRequestRepository = clubRequestRepository;

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

            //prihvatanje zahteva
            clubRequest.AcceptRequest();
            _clubRequestRepository.Update(clubRequest);

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
            var clubRequest = new ClubRequest(requestDto.TouristId, requestDto.ClubId);
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
            foreach(var club in clubRequests)
            {
                ClubRequestDto dto = new();
                dto.ClubId = club.ClubId;
                dto.TouristId = club.TouristId;
                dto.Id = club.Id;
                dto.Status = (API.Dtos.ClubRequestStatus)(API.Dtos.ClubInvitationStatus)Enum.Parse(typeof(API.Dtos.ClubInvitationStatus), club.Status.ToString());
                dtos.Add(dto);
            }
            return Result.Ok(dtos);
        }
    }
}
