namespace Explorer.Payments.API.Dtos.Sales;

public class SalesDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime EndsAt { get; set; }
    public int Discount { get; set; }
    public List<int> TourIds { get; set; } = new();
}
