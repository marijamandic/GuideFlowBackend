using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal
{
    public interface IInternalPurchaseTokenService
    {
        Result<PagedResult<TourPurchaseTokenDto>> GetTokensByTouristId(int touristId);
        Result<TourPurchaseTokenDto> GetTokenByTouristAndTourId(int touristId, int tourId);
        int GetNumOfPurchases(long tourId);
    }
}
