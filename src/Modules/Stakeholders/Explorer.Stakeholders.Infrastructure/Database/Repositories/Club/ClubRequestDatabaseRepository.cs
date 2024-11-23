using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories.Club
{
    public class ClubRequestDatabaseRepository : IClubRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubRequestDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ClubRequest> GetAll()
        {
            return _dbContext.ClubRequests.ToList();
        }
        public ClubRequest GetById(long id)
        {
            return _dbContext.ClubRequests.FirstOrDefault(cr => cr.Id == id);
        }

        public List<ClubRequest> GetByStatus(ClubRequestStatus status)
        {
            return _dbContext.ClubRequests
                .Where(cr => cr.Status == status)
                .ToList();
        }

        public ClubRequest Create(ClubRequest clubRequest)
        {
            _dbContext.ClubRequests.Add(clubRequest);
            _dbContext.SaveChanges();
            return clubRequest;
        }

        public void Update(ClubRequest clubRequest)
        {
            _dbContext.ClubRequests.Update(clubRequest);
            _dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var clubRequest = GetById(id);
            if (clubRequest != null)
            {
                _dbContext.ClubRequests.Remove(clubRequest);
                _dbContext.SaveChanges();
            }
        }

        public List<ClubRequest> GetByTouristId(long touristId)
        {
            return _dbContext.ClubRequests
                .Where(cr => cr.TouristId == touristId)
                .ToList();
        }

        public List<ClubRequest> GetRequestsByClubId(long clubId)
        {
            return _dbContext.ClubRequests
                .Where(cr => cr.ClubId == clubId)
                .ToList();
        }

    }
}
