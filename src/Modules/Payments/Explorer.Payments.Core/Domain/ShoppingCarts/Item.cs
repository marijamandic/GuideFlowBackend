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
        Validate();
    }

    public void Validate()
    {
        if (!Enum.IsDefined(typeof(ProductType), Type)) throw new ArgumentException("Invalid Product Type");
        if (string.IsNullOrWhiteSpace(ProductName)) throw new ArgumentException("Invalid Product Name");
        if (AdventureCoin < 0) throw new ArgumentException("Invalid Adventure Coin");
    }
}

public enum ProductType
{
    Tour,
    Bundle
}