using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories.Club
{
    public class ClubMemberDatabaseRepository : IClubMemberRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubMemberDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClubMember GetById(long clubId, long userId)
        {
            return _dbContext.ClubMembers
                .FirstOrDefault(cm => cm.ClubId == clubId && cm.UserId == userId);
        }

        public List<ClubMember> GetAll()
        {
            return _dbContext.ClubMembers.ToList();
        }


        public List<ClubMember> GetByClubId(long clubId)
        {
            return _dbContext.ClubMembers
                .Where(cm => cm.ClubId == clubId)
                .ToList();
        }

        public ClubMember Create(ClubMember clubMember)
        {
            _dbContext.ClubMembers.Add(clubMember);
            _dbContext.SaveChanges();
            return clubMember;
        }

        public void Update(ClubMember clubMember)
        {
            _dbContext.ClubMembers.Update(clubMember);
            _dbContext.SaveChanges();
        }

        public void Delete(long clubId, long userId)
        {
            var clubMember = GetById(clubId, userId);
            if (clubMember != null)
            {
                _dbContext.ClubMembers.Remove(clubMember);
                _dbContext.SaveChanges();
            }
        }

        public List<ClubMember> GetByUserId(long userId)
        {
            return _dbContext.ClubMembers
                .Where(cm => cm.UserId == userId)
                .ToList();
        }
    }
}
