using Explorer.Payments.Core.Domain.ShoppingCarts;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IShoppingCartRepository
{
    ShoppingCart GetByTouristId(int touristId);
    void Save(ShoppingCart shoppingCart);
    ShoppingCart Create(ShoppingCart shoppingCart);
}
