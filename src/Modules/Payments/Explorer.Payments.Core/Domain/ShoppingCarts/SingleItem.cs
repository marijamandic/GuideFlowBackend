using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class SingleItem : Entity
{
    public long ShoppingCartId { get; private set; }
    public long TourId { get; private set; }
    public string TourName { get; set; }
    public int Price { get; set; }

    public SingleItem(long id, long shoppingCartId, long tourId, string tourName, int price)
    {
        Id = id;
        ShoppingCartId = shoppingCartId;
        TourId = tourId;
        TourName = tourName;
        Price = price;
    }
}
