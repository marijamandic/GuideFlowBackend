using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Club
{
    public class ClubMember : Entity
    {
        public long ClubId { get; private set; }
        public long UserId { get; private set; }
        public DateTime JoinedDate { get; private set; }

        // Navigation properties 
        public Club Club { get; private set; }
        public User User { get; private set; }

        protected ClubMember() { }

        public ClubMember(long clubId, long userId)
        {
            if (clubId <= 0)
                throw new ArgumentException("ClubId must be a positive number.", nameof(clubId));
            if (userId <= 0)
                throw new ArgumentException("UserId must be a positive number.", nameof(userId));

            ClubId = clubId;
            UserId = userId;
            JoinedDate = DateTime.UtcNow;
        }
    }
}
