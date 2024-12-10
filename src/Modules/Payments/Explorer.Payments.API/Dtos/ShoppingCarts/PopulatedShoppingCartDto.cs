namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class PopulatedShoppingCartDto
{
	public int Id { get; set; }
	public int TouristId { get; set; }
	public List<PopulatedItemDto> Items { get; set; } = new();
}
