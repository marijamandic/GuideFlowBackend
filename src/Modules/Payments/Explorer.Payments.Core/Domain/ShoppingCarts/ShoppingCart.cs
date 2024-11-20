using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class ShoppingCart : Entity
{
    private List<Item> _items = new();
    public long TouristId { get; private set; }
    public IReadOnlyList<Item> Items => new List<Item>(_items);

    public ShoppingCart(long touristId)
    {
        TouristId = touristId;
    }

    public void AddToCart(Item item)
    {
        if (_items.Select(i => i.ProductId).Contains(item.ProductId)) throw new ArgumentException("Tour already in cart");
        _items.Add(item);
    }

    public void RemoveFromCart(Item item)
    {
        _items.Remove(item);
    }

    public void ClearCart()
    {
        _items.Clear();
    }

    public Item GetById(long itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item is null) throw new ArgumentException("Item does not exits");
        return item;
    }
}
