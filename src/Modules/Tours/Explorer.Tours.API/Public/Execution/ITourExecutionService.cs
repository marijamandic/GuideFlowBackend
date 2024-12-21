using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Execution;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Execution
{
    public interface ITourExecutionService
    {
        Result<TourExecutionDto> Update(UpdateTourExecutionDto updateTourExecutionDto);
        Result<TourExecutionDto> Create(CreateTourExecutionDto createTourExecutionDto);
        Result<TourExecutionDto> GetSessionsByUserId(long userId);
        Result<TourExecutionDto> CompleteSession(long userId);
        Result<TourExecutionDto> AbandonSession(long userId);
        Result<TourExecutionDto> Get(long id);
        //Result<PagedResult<TourExecutionDto>> GetPaged(int page , int pageSize);
        Task<int> GetTourCompletionPercentageAsync(long tourExecutionId);
        Result<PagedResult<TourExecutionDto>> GetPaged(int page , int pageSize);
        Result<List<long>> GetCompletedToursByTourist(int id);
    }
}
