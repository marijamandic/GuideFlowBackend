using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class Item : Entity
{
    public long ShoppingCartId { get; private set; }
    public ProductType Type { get; private set; }
    public long ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int AdventureCoin { get; private set; }

    public Item(long id, long shoppingCartId, ProductType type, long productId, string productName, int adventureCoin)
    {
        Id = id;
        ShoppingCartId = shoppingCartId;
        Type = type;
        ProductId = productId;
        ProductName = productName;
        AdventureCoin = adventureCoin;
    }
}

public enum ProductType
{
    Tour,
    Bundle
}