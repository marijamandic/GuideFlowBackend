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
    public interface IInternalTourPurchaseTokenService
    {
        Result<PagedResult<TourPurchaseTokenDto>> GetAllByTouristId(int touristId);
        Result<TourPurchaseTokenDto> GetByTouristAndTourId(int touristId, int tourId);
    }
}
