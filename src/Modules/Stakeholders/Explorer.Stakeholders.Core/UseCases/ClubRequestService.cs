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
    }
}
