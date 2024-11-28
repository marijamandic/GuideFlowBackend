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
    public class TourPurchaseTokenDatabaseRepository : ITourPurchaseTokenRepository
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly DbSet<TourPurchaseToken> _purchaseTokens;

        public TourPurchaseTokenDatabaseRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
            _purchaseTokens = _paymentsContext.Set<TourPurchaseToken>();
        }

        public TourPurchaseToken Create(TourPurchaseToken entity)
        {
            _purchaseTokens.Add(entity);
            _paymentsContext.SaveChanges();
            return entity;
        }

        public PagedResult<TourPurchaseToken> GetTokensByTouristId(long touristId)
        {
            List<TourPurchaseToken> touristPurchaseTokens=_purchaseTokens.Where(pt=>pt.TouristId == touristId).ToList();
            if (touristPurchaseTokens.Count == 0) throw new KeyNotFoundException("Not found any token for touristId: " + touristId);
            return new PagedResult<TourPurchaseToken>(touristPurchaseTokens,touristPurchaseTokens.Count);
        }

        public TourPurchaseToken GetTokenByTouristAndTourId(long touristId, long tourId)
        {
            TourPurchaseToken? tourPurchaseToken = _purchaseTokens.FirstOrDefault(pt => pt.TouristId == touristId && pt.TourId == tourId);
            if (tourPurchaseToken == null) throw new KeyNotFoundException("Not found any token for touristId: " + touristId + " and tourId: " + tourId);
            return tourPurchaseToken;
        }
    }
}
