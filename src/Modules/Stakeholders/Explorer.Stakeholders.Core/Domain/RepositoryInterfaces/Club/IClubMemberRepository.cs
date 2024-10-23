using Explorer.Stakeholders.Core.Domain.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club
{
    public interface IClubMemberRepository
    {
        ClubMember GetById(long clubId, long userId);
        List<ClubMember> GetByClubId(long clubId);
        ClubMember Create(ClubMember clubMember);
        void Update(ClubMember clubMember);
        void Delete(long clubId, long userId);
    }
}
