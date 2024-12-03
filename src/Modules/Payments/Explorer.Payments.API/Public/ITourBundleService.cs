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
    public interface ITourBundleService
    {
        Result<PagedResult<TourBundleDto>> GetAll();

        Result<TourBundleDto> Get(int id);

        Result<TourBundleDto> Create(TourBundleDto tourBundleDto);

        Result<TourBundleDto> Delete(long tourBundleId);

        Result<TourBundleDto> Modify(TourBundleDto tourBundleDto);

        Result<TourBundleDto> Publish(long tourBundleId);

        Result<TourBundleDto> Archive(long tourBundleId);
    }
}
