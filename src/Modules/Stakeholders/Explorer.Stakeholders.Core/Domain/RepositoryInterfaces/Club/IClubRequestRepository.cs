using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.Club;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club
{
    public interface IClubRequestRepository
    {
        List<ClubRequest> GetAll();
        ClubRequest GetById(long id);
        List<ClubRequest> GetByStatus(ClubRequestStatus status); 
        ClubRequest Create(ClubRequest clubRequest); 
        void Update(ClubRequest clubRequest); 
        void Delete(long id);
        List<ClubRequest> GetByTouristId(long touristId);
    }
}
