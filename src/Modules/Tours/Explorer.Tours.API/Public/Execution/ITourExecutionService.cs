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
        Result<TourExecutionDto> Get(long id);
        //Result<PagedResult<TourExecutionDto>> GetPaged(int page , int pageSize);
    }
}
