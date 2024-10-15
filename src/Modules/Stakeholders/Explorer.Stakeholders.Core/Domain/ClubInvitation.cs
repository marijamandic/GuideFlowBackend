using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum ClubInvitationStatus
    {
        PENDING,
        ACCEPTED,
        DECLINED,
        CANCELLED
    }

    public class ClubInvitation : Entity
    {
        public long ClubId { get; private set; }
        public long TouristID { get; private set; }
        public ClubInvitationStatus Status { get; private set; }

        public ClubInvitation(long clubId, long touristID, ClubInvitationStatus status)
        {
            if (clubId <= 0)
            {
                throw new ArgumentException("ClubId must be a positive number.", nameof(clubId));
            }

            if (touristID <= 0)
            {
                throw new ArgumentException("TouristID must be a positive number.", nameof(touristID));
            }

            ClubId = clubId;
            TouristID = touristID;
            Status = status;
        }

        public void ChangeStatus(ClubInvitationStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
