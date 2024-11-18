using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ITourPurchaseTokenService
    {
        Result<TourPurchaseTokenDto> Create(TourPurchaseTokenDto tourPurchaseToken);
        Result<PagedResult<TourPurchaseTokenDto>> GetAllByTouristId(int touristId);
        Result<TourPurchaseTokenDto> GetByTouristAndTourId(int touristId, int tourId);
    }
}
