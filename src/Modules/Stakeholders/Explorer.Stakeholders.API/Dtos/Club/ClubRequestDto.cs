namespace Explorer.Stakeholders.API.Dtos.Club;

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
    public DateTime CreatedAt { get; set; }
    public bool IsOpened { get; set; }
    public long OwnerId { get; set; }
    public string ClubName { get; set; }
    public string TouristName { get; set; }
}
