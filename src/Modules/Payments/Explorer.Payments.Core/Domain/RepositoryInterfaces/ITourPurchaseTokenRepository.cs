using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.PurchaseTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ITourPurchaseTokenRepository
    {
        TourPurchaseToken Create(TourPurchaseToken entity);
        TourPurchaseToken GetByTourAndTouristId(long tourId, long touristId);
        PagedResult<TourPurchaseToken> GetAllByTouristId(long touristId);
    }
}
