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
    public long Id { get; set; }
    public long TouristId { get; set; }
    public long ClubId { get; set; }
    public ClubRequestStatus Status { get; set; }
}
