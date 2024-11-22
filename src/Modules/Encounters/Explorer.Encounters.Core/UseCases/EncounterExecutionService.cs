using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterExecutionService :IEncounterExecutionService
    {
        public Result<EncounterExecutionDto> Create(EncounterDto encounterDto)
        {
            throw new NotImplementedException();
        }
        public Result<EncounterExecutionDto> Update(EncounterDto encounterDto)
        {
            throw new NotImplementedException();
        }
        public Result Delete(int id) { throw new NotImplementedException(); }

        public Result<EncounterExecutionDto> Get(long id) { throw new NotImplementedException(); }
        public Result<PagedResult<EncounterExecutionDto>> GetPaged(int page, int pageSize) { throw new NotImplementedException(); }
    }
}
