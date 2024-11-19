namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class SingleItemInputDto
{
    public int TourId { get; set; }
    public required string TourName { get; set; }
    public int AdventureCoin { get; set; }
}
