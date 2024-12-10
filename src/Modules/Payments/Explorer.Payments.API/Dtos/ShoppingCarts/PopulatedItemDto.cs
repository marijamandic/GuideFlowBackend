namespace Explorer.Payments.API.Dtos.ShoppingCarts;

public class PopulatedItemDto
{
	public int Id { get; set; }
	public int ShoppingCartId { get; set; }
	public ProductType Type { get; set; }
	public required object Product { get; set; }
	public required string ProductName { get; set; }
	public int AdventureCoin { get; set; }
}
