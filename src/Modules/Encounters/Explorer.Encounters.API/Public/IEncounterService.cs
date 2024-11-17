using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        public Result<EncounterDto> Create(EncounterDto encounterDto);
        public Result<EncounterDto> Update(EncounterDto encounterDto);
        public Result Delete(int  id);
        public Result<EncounterDto> Get(int id);
        public Result<PagedResult<EncounterDto>> GetPaged(int page, int pageSize);
    }
}
