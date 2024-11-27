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
    public class ClubMemberService : IClubMemberService
    {
        private readonly IClubMemberRepository _clubMemberRepository;
        private readonly IClubService _clubService;

        private ClubMemberService(IClubService clubService) { 
            _clubService = clubService;
        }

        public ClubMemberService(IClubMemberRepository clubMemberRepository)
        {
            _clubMemberRepository = clubMemberRepository;
        }

        public Result<ClubMemberDto> AddMember(long clubId, long userId)
        {
            var newMember = new ClubMember(clubId, userId);
            _clubMemberRepository.Create(newMember);
            var memberDto = new ClubMemberDto { ClubId = clubId, UserId = userId };
            return Result.Ok(memberDto);
        }

        public Result<List<ClubMemberDto>> GetMembersByClub(long clubId)
        {
            var members = _clubMemberRepository.GetByClubId(clubId);
            var memberDtos = members.Select(m => new ClubMemberDto { ClubId = m.ClubId, UserId = m.UserId }).ToList();
            return Result.Ok(memberDtos);
        }

        public Result RemoveMember(long clubId, long userId)
        {
            _clubMemberRepository.Delete(clubId, userId);
            return Result.Ok();
        }
    }
}
