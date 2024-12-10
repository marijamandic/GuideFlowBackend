namespace Explorer.Payments.API.Dtos;

public class TourBundleDto
{
    public long Id { get; set; }    
    public required string Name { get; set; }
    public double Price { get; set; }
    public BundleStatus Status { get; set; }
    public int AuthorId { get; set; }
    public List<int> TourIds { get; set; } = new();
    public List<TourDetailsDto>? Tours { get; set; } = new();
}
public enum BundleStatus
{
    Draft,
    Published,
    Archieved
}
