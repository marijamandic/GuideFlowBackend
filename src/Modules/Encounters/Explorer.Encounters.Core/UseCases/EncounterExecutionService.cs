using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterExecutionService : BaseService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
    {
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;

        public EncounterExecutionService(IEncounterExecutionRepository encounterExecutionRepository, IMapper mapper) : base(mapper)
        {
            _encounterExecutionRepository = encounterExecutionRepository;

        }

        public Result<EncounterExecutionDto> Create(EncounterDto encounterDto)
        {
            // var allExecution = _encounterExecutionRepository.
            // dobavi listu execution-a
            if(encounterDto.Id != null && encounterDto.EncounterType.Equals(Domain.EncounterType.Social))
            {

            }
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
