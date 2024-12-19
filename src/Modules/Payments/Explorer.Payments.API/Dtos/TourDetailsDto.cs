namespace Explorer.Payments.API.Dtos;

public class TourDetailsDto
{
	public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public TourLevel Level { get; set; }
	public List<string> Tags { get; set; } = new();
}

public enum TourLevel
{
	Easy,
	Advanced,
	Expert
}
