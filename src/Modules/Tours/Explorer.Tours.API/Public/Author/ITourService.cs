using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Author
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Get(int id);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(int id);
        Result<TourDto> AddCheckpoint(int tourId, CheckpointDto checkpoint);
        Result<TourDto> AddTransportDurations(int id, List<TransportDurationDto> transportDurations);
        Result<TourDto> Archive(int id);   
        Result<TourDto> Publish(int id);
        Result<TourDto> UpdateLength(int id, double length);
        Result<List<long>> GetTourIdsByAuthorId(int authorId);
    }
}
