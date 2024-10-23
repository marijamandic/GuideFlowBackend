using Explorer.BuildingBlocks.Core.Domain;
using System;


namespace Explorer.Stakeholders.Core.Domain.Club;

public enum ClubRequestStatus
{
    PENDING,
    ACCEPTED,
    DECLINED,
    CANCELLED
}

public class ClubRequest : Entity
{

    public long TouristId { get; private set; }
    public long ClubId { get; private set; }
    public ClubRequestStatus Status { get; private set; }

    public ClubRequest(long touristId, long clubId)
    {

        TouristId = touristId;
        ClubId = clubId;
        Status = ClubRequestStatus.PENDING;
    }

    public void AcceptRequest()
    {
        if (Status != ClubRequestStatus.PENDING)
            throw new InvalidOperationException("Only pending requests can be accepted.");

        Status = ClubRequestStatus.ACCEPTED;
    }

    public void DeclineRequest()
    {
        if (Status != ClubRequestStatus.PENDING)
            throw new InvalidOperationException("Only pending requests can be declined.");

        Status = ClubRequestStatus.DECLINED;
    }

    public void CancelRequest()
    {
        if (Status != ClubRequestStatus.PENDING)
            throw new InvalidOperationException("Only pending requests can be cancelled.");

        Status = ClubRequestStatus.CANCELLED;
    }

}
