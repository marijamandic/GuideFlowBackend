using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;
public interface ITourObjectService
{
    Result<PagedResult<TourObjectDto>> GetPaged(int page, int pageSize);
    Result<TourObjectDto> Create(TourObjectDto tourObject);
    Result<TourObjectDto> Update(TourObjectDto tourObject);

}