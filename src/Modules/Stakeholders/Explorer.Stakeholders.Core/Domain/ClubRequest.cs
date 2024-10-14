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

    public int TouristId { get; private set; }
    public int ClubId { get; private set; }
    public ClubRequestStatus Status { get; private set; }

    public ClubRequest(int touristId, int clubId)
	{

        TouristId = touristId;
        ClubId = clubId;
        Status = ClubRequestStatus.PENDING;
	}

}
