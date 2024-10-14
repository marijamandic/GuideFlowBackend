namespace Explorer.Stakeholders.API.Dtos;

public enum ClubRequestStatus
{
    PENDING,
    ACCEPTED,
    DECLINED,
    CANCELLED
}


public class ClubRequestDto
{
    public int TouristId { get; set; }
    public int ClubId { get; set; }
    public ClubRequestStatus Status { get; set; }
}
