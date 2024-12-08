using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class ShoppingCartDatabaseRepository : IShoppingCartRepository
{
    private readonly PaymentsContext _paymentsContext;
    private readonly DbSet<ShoppingCart> _shoppingCarts;

    public ShoppingCartDatabaseRepository(PaymentsContext paymentsContext)
    {
        _paymentsContext = paymentsContext;
        _shoppingCarts = _paymentsContext.Set<ShoppingCart>();
    }

    public ShoppingCart GetByTouristId(int touristId)
    {
        var shoppingCart = _shoppingCarts
            .Include(sc => sc.Items)
            .FirstOrDefault(sc => sc.TouristId == touristId);

        if (shoppingCart is null) throw new ArgumentException("Tourist ID mismatch");
        return shoppingCart;
    }
    public ShoppingCart Create(ShoppingCart entity)
    {
        _shoppingCarts.Add(entity);
        _paymentsContext.SaveChanges();
        return entity;
    }
    public void Save(ShoppingCart shoppingCart)
    {
        _paymentsContext.Entry(shoppingCart).State = EntityState.Modified;
        _paymentsContext.SaveChanges();
    }
}
