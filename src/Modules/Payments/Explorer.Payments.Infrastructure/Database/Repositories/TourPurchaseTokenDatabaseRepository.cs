using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.PurchaseTokens;
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
        private readonly DbSet<TourPurchaseToken> _tourPurchaseTokens;

        public TourPurchaseTokenDatabaseRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
            _tourPurchaseTokens = _paymentsContext.Set<TourPurchaseToken>();
        }

        public TourPurchaseToken Create(TourPurchaseToken entity)
        {
            _tourPurchaseTokens.Add(entity);
            _paymentsContext.SaveChanges();
            return entity;
        }

        public PagedResult<TourPurchaseToken> GetAllByTouristId(long touristId)
        {
            List<TourPurchaseToken> touristPurchaseTokens=_tourPurchaseTokens.Where(tpt=>tpt.TouristId == touristId).ToList();
            if (touristPurchaseTokens.Count == 0) throw new KeyNotFoundException("Not found any token for touristId: " + touristId);
            return new PagedResult<TourPurchaseToken>(touristPurchaseTokens,touristPurchaseTokens.Count);
        }

        public TourPurchaseToken GetByTourAndTouristId(long tourId, long touristId)
        {
            TourPurchaseToken? tourPurchaseToken= _tourPurchaseTokens.FirstOrDefault(tpt => tpt.TouristId == touristId && tpt.TourId == tourId);
            if (tourPurchaseToken == null) throw new KeyNotFoundException("Not found token with tourId: " + tourId + " and touristId: " + touristId);
            return tourPurchaseToken;
        }
    }
}
