using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.Club;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club
{
    public interface IClubInvitationRepository
    {
        ClubInvitation GetById(long id);
        List<ClubInvitation> GetByStatus(ClubInvitationStatus status);
        ClubInvitation Create(ClubInvitation clubInvitation);
        void Update(ClubInvitation clubInvitation);
        void Delete(long id);
        List<ClubInvitation> GetAll();
        List<ClubInvitation> GetByClubId(long clubId);

    }
}
