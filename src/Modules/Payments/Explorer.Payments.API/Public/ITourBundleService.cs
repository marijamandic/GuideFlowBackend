using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ITourBundleService
    {
        Result<TourBundleDto> Create(TourBundleDto tourBundleDto);

        Result<TourBundleDto> Delete(long tourBundleId);

        Result<TourBundleDto> AddTour(long tourBundleId, long tourId);

        Result<TourBundleDto> RemoveTour(long tourBundleId, long tourId);

        Result<TourBundleDto> Publish(long tourBundleId);

        Result<TourBundleDto> Archive(long tourBundleId);
    }
}
