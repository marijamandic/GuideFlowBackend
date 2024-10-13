using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;
public interface ITourObjectService
{
    Result<TourObjectDto> Create(TourObjectDto tourObject);
}