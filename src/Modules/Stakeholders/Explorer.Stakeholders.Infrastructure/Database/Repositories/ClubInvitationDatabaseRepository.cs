using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubInvitationDatabaseRepository : IClubInvitationRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubInvitationDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClubInvitation GetById(long id)
        {
            return _dbContext.ClubInvitations.FirstOrDefault(ci => ci.Id == id);
        }

        public List<ClubInvitation> GetByStatus(ClubInvitationStatus status)
        {
            return _dbContext.ClubInvitations
                .Where(ci => ci.Status == status)
                .ToList();
        }

        public ClubInvitation Create(ClubInvitation clubInvitation)
        {
            _dbContext.ClubInvitations.Add(clubInvitation);
            _dbContext.SaveChanges();
            return clubInvitation;
        }

        public void Update(ClubInvitation clubInvitation)
        {
            _dbContext.ClubInvitations.Update(clubInvitation);
            _dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var clubInvitation = GetById(id);
            if (clubInvitation != null)
            {
                _dbContext.ClubInvitations.Remove(clubInvitation);
                _dbContext.SaveChanges();
            }
        }
    }
}
