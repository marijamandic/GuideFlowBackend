using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public class IEncounterExecutionService
    {
        public Result<EncounterExecutionDto> Create(EncounterDto encounterDto);
        public Result<EncounterExecutionDto> Update(EncounterDto encounterDto);
        public Result Delete(int id);
        public Result<EncounterExecutionDto> Get(long id);
        public Result<PagedResult<EncounterExecutionDto>> GetPaged(int page, int pageSize);
    }
}
