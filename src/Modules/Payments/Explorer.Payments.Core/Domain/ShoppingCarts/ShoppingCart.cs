using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class ShoppingCart : Entity
{
    private List<SingleItem> _singleItems = new();

    public long TouristId { get; private set; }
    public IReadOnlyList<SingleItem> SingleItems => new List<SingleItem>(_singleItems);

    public ShoppingCart(long touristId)
    {
        TouristId = touristId;
    }

    public void AddToCart(SingleItem item)
    {
        if (_singleItems.Select(i => i.TourId).Contains(item.TourId)) throw new ArgumentException("Tour already in cart");
        _singleItems.Add(item);
    }

    public void RemoveFromCart(SingleItem item)
    {
        _singleItems.Remove(item);
    }

    public SingleItem GetById(long itemId)
    {
        var item = _singleItems.FirstOrDefault(i => i.Id == itemId);
        if (item is null) throw new ArgumentException("Item does not exits");
        return item;
    }
}
