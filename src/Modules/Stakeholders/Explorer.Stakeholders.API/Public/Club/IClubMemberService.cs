using Explorer.Stakeholders.API.Dtos.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public.Club
{
    public interface IClubMemberService
    {
        Result<ClubMemberDto> AddMember(long clubId, long userId);
        Result<List<ClubMemberDto>> GetMembersByClub(long clubId);
        Result RemoveMember(long clubId, long userId);
    }
}
