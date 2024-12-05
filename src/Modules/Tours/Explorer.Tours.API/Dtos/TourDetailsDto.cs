namespace Explorer.Tours.API.Dtos;

public class TourDetailsDto
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public Level Level { get; set; }
    public List<string> Tags { get; set; } = new();
}
