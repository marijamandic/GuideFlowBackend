namespace Explorer.Payments.API.Dtos.Sales;

public class SalesInputDto
{
    public int AuthorId { get; set; }
    public DateTime EndsAt { get; set; }
    public int Discount { get; set; }
    public List<int> TourIds { get; set; } = new();
}
