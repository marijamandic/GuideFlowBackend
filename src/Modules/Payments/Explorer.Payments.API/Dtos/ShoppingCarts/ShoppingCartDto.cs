namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class ShoppingCartDto
{
    public int Id { get; set; }
    public int TouristId { get; set; }
    public List<ItemDto> Items { get; set; } = new();
}
