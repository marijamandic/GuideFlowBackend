using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class PurchaseTokenDatabaseRepository : IPurchaseTokenRepository
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly DbSet<PurchaseToken> _purchaseTokens;

        public PurchaseTokenDatabaseRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
            _purchaseTokens = _paymentsContext.Set<PurchaseToken>();
        }

        public PurchaseToken Create(PurchaseToken entity)
        {
            _purchaseTokens.Add(entity);
            _paymentsContext.SaveChanges();
            return entity;
        }

        public PagedResult<PurchaseToken> GetTourTokensByTouristId(long touristId)
        {
            List<PurchaseToken> touristPurchaseTokens=_purchaseTokens.Where(pt=>pt.TouristId == touristId && pt.Type == Core.Domain.ShoppingCarts.ProductType.Tour).ToList();
            if (touristPurchaseTokens.Count == 0) throw new KeyNotFoundException("Not found any tour token for touristId: " + touristId);
            return new PagedResult<PurchaseToken>(touristPurchaseTokens,touristPurchaseTokens.Count);
        }
    }
}
