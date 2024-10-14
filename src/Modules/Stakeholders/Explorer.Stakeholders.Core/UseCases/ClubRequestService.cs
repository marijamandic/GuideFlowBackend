using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
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
        public ClubRequestService(IMapper mapper) : base(mapper)
        {
        }

        public Result<ClubRequestDto> AcceptMembershipRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubRequestDto> CancelMembershipRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubRequestDto> DeclineMembershipRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubRequestDto> GetRequestStatus(int requestId)
        {
            throw new NotImplementedException();
        }

        public Result<ClubRequestDto> SubmitMembershipRequest(ClubRequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
