using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
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
        PagedResult<TourPurchaseToken> GetTokensByTouristId(long touristId);
        TourPurchaseToken GetTokenByTouristAndTourId(long touristId, long tourId);
    }
}
