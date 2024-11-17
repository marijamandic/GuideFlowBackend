using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class ShoppingCart : Entity
{
    private List<SingleItem> _singleItems = new();
    public IReadOnlyList<SingleItem> SingleItems => new List<SingleItem>(_singleItems);
}
