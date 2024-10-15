using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubInvitationRepository
    {
        ClubInvitation GetById(long id);  
        List<ClubInvitation> GetByStatus(ClubInvitationStatus status);  
        ClubInvitation Create(ClubInvitation clubInvitation);  
        void Update(ClubInvitation clubInvitation);  
        void Delete(long id); 
    }
}
