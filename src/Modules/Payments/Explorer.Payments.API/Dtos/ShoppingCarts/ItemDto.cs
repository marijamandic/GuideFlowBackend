namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class ItemDto
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public ProductType Type { get; set; }
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public object? Product { get; set; }
    public int AdventureCoin { get; set; }
}

public enum ProductType
{
    Tour,
    Bundle
}