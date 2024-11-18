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
        _singleItems.Add(item);
    }
}
