namespace Explorer.Payments.API.Dtos;

public class PopulatedTourBundleDto
{
	public int Id { get; set; }
    public required string Name { get; set; }
	public double Price { get; set; }
    public BundleStatus Status { get; set; }
    public int AuthorId { get; set; }
    public List<object> Tours { get; set; } = new();
}
