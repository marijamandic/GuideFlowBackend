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
		foreach (var i in _items)
			if (i.ProductId == item.ProductId && i.Type == item.Type)
				throw new InvalidOperationException("Tour already in cart");

		item.Validate();
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
