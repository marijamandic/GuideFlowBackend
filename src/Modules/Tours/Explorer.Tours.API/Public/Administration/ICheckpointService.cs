using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System.Collections.Generic;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ICheckpointService
    {
        Result<CheckpointDto> Create(CheckpointDto checkpoint);
        Result<CheckpointDto> Update(CheckpointDto checkpoint);
        Result Delete(int id);
        Result<CheckpointDto> Get(int id);
        Result<PagedResult<CheckpointDto>> GetPaged(int page, int pageSize);
    }
}
