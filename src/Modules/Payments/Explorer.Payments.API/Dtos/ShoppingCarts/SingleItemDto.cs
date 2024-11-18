namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class SingleItemDto
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public int TourId { get; set; }
    public required string TourName { get; set; }
    public int Price { get; set; }
}
