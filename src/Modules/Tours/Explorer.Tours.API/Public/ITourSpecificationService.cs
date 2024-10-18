using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourSpecificationService
    {
        Result<PagedResult<TourSpecificationDto>> GetPaged(int page, int pageSize);
        Result<TourSpecificationDto> CreateTourSpecifications(TourSpecificationDto tourSpecificationDto);
        Result<IEnumerable<TourSpecificationDto>> GetAllTourSpecifications();
        Result UpdateTourSpecifications(TourSpecificationDto tourSpecificationDto);
        Result DeleteTourSpecifications(long id);
        Result<TourSpecificationDto> GetTourSpecificationsByUserId(long userId);
    }
}
