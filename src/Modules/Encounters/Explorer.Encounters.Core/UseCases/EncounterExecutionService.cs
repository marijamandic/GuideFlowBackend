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
        public Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterDto)
        {
            // var allExecution = _encounterExecutionRepository.
            // dobavi listu execution-a
           /* if(encounterDto.Id != null && encounterDto.EncounterType.Equals(Domain.EncounterType.Social))
            {

            }*/
            throw new NotImplementedException();
        }
        public Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterExecutionDto)
        {
            var encounter = _encounterExecutionRepository.Update(MapToDomain(encounterExecutionDto));
            return MapToDto(encounter);

        }
        public Result Delete(int id) {

            try
            {
                _encounterExecutionRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            
        }

        public Result<EncounterExecutionDto> Get(long id)
        {
            var encounterExecution= _encounterExecutionRepository.Get(id);
            if (encounterExecution is null)
                return Result.Fail(FailureCode.NotFound);
            return MapToDto(encounterExecution);
        }
        public Result<PagedResult<EncounterExecutionDto>> GetPaged(int page, int pageSize) {
            var encountersExecutions = _encounterExecutionRepository.GetPaged(page, pageSize);
            return MapToDto(encountersExecutions);
        }
    
       
     
    
    }
}
