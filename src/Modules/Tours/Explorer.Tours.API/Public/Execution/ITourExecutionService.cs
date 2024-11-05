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
        void CompleteSession(long userId);
        void AbandonSession(long userId);


    }
}
