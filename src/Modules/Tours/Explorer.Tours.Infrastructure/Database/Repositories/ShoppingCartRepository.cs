using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Shopping;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : CrudDatabaseRepository<ShoppingCart, ToursContext>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ToursContext dbContext) : base(dbContext) { }
        public new ShoppingCart Update(ShoppingCart shoppingCart)
        {
            DbContext.Entry(shoppingCart).State = EntityState.Modified;
            DbContext.SaveChanges();
            return shoppingCart;
        }

        public new ShoppingCart Get(long id) {
            var shoppingCart = DbContext.ShoppingCarts.Where(sc => sc.Id == id)
                .Include(sc=>sc.Items!)
                .FirstOrDefault();

            if (shoppingCart == null) { throw new KeyNotFoundException("Not found: " + id); }
            return shoppingCart;
        }

        public ShoppingCart GetByUserId(long touristId)
        {
            var shoppingCart = DbContext.ShoppingCarts.Where(sc => sc.TouristId == touristId)
                .Include(sc=>sc.Items!)
                .FirstOrDefault();

            if(shoppingCart == null) { throw new KeyNotFoundException("Not found: " + touristId); }
            return shoppingCart;
        }
    }
}
