using Explorer.BuildingBlocks.Core.Domain;
using System;


namespace Explorer.Stakeholders.Core.Domain;

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

}
