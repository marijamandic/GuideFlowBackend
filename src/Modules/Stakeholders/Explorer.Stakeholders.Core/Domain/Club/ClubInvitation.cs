using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Club
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
        [Column("ClubId")]
        public long ClubId { get; private set; }

        [Column("TouristID")]
        public long TouristID { get; private set; }

        [Column("Status")]
        public ClubInvitationStatus Status { get; private set; }

        protected ClubInvitation() { }

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

        public void AcceptInvitation()
        {
            if (Status != ClubInvitationStatus.PENDING)
                throw new InvalidOperationException("Only pending invitations can be accepted.");
            Status = ClubInvitationStatus.ACCEPTED;
        }

        public void DeclineInvitation()
        {
            if (Status != ClubInvitationStatus.PENDING)
                throw new InvalidOperationException("Only pending invitations can be declined.");
            Status = ClubInvitationStatus.DECLINED;
        }

        public void CancelInvitation()
        {
            if (Status != ClubInvitationStatus.PENDING)
                throw new InvalidOperationException("Only pending invitations can be cancelled.");
            Status = ClubInvitationStatus.CANCELLED;
        }

        public void ChangeStatus(ClubInvitationStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateDetails(long clubId, long touristId, ClubInvitationStatus status)
        {
            ClubId = clubId;
            TouristID = touristId;
            Status = status;
        }
    }
}
