namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class InputDto
{
    public ProductType Type { get; set; }
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public int AdventureCoin { get; set; }
}
