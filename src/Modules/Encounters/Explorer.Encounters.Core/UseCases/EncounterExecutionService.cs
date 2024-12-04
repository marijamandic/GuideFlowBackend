using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterExecutionService : BaseService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
    {
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;
        private readonly IEncountersRepository _encounterRepository;
        private readonly IInternalTouristService _internalTouristService;
        public EncounterExecutionService(IEncounterExecutionRepository encounterExecutionRepository, IEncountersRepository encountersRepository, IMapper mapper,IInternalTouristService internalTouristService) : base(mapper)
        {
            _encounterExecutionRepository = encounterExecutionRepository;
            _encounterRepository = encountersRepository;
            _internalTouristService = internalTouristService;
        }
        public Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterExecutionDto)
        {
            var allExecutions = _encounterExecutionRepository.GetAll();
            var execution = MapToDomain(encounterExecutionDto);
            var encounter = _encounterRepository.Get(encounterExecutionDto.EncounterId);

            //da li je korisnik u blizini aktivira taj izazov ili da mu se pridruzi
            if (execution.IsTouristNear(encounter))
            {
                //ako je social enc napravi ga i proveri da l ima dovoljno ljudi, ako ima zavrsi ga
                if (encounter is SocialEncounter socialEncounter)
                {
                   //var encounterExecution = new EncounterExecution(encounterExecutionDto.EncounterId, encounterExecutionDto.UserId);
                    _encounterExecutionRepository.Create(execution);

                    //CompleteSocialEncounter(socialEncounter);
                    
                    return MapToDto(execution);

                }else if (!allExecutions.Contains(execution)) // ako ne postoji onda se pravi nova ex
                {
                    var encounterExecution = new EncounterExecution(encounterExecutionDto.EncounterId, encounterExecutionDto.UserId);
                    _encounterExecutionRepository.Create(encounterExecution);
                    return MapToDto(encounterExecution);
                }
                return Result.Fail("Execution not created, already exists");
            }

            return Result.Fail("Tourist can't activate or join this encounter");
        }


        public Result<EncounterExecutionDto> CompleteSocialEncounter(EncounterExecutionDto encounterExecutionDto)
        {
            var encounter = _encounterRepository.Get(encounterExecutionDto.EncounterId);
            //ako je social enc napravi ga i proveri da l ima dovoljno ljudi, ako ima zavrsi ga
            if (encounter is SocialEncounter socialEncounter)
            {
                
                var activeExecutions = _encounterExecutionRepository
            .GetByEncounterId(socialEncounter.Id)
            .Where(e => e.IsTouristNear(e.Encounter))
            .ToList();

                foreach (var execution in activeExecutions)
                {
                    execution.CountParticipants(activeExecutions.Count);
                    execution.CompleteSocialEncounter();
                    Update(MapToDto(execution));
                }
            }

            return encounterExecutionDto;

        }

       /* public Result CheckActiveParticipants(int executionId,int longitude,int latitude)
        {
            var execution = Get(executionId);
            var encounter = _encounterRepository.Get(execution.encounterId);
            if (execution.isTouristNear());
        }*/

        public Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterExecutionDto)
        {
            var encounterExecution = _encounterExecutionRepository.Get(encounterExecutionDto.Id);
            var encounter = _encounterRepository.Get(encounterExecutionDto.EncounterId);
            if(encounterExecution.Encounter.EncounterType == Domain.EncounterType.Location)
            {
                if (encounterExecution.IsHiddenLocationFound(encounterExecutionDto.UserLatitude, encounterExecutionDto.UserLongitude, encounter))
                    encounterExecution.Complete(MapToDomain(encounterExecutionDto));
                    
                else
                    return Result.Fail("Hidden Location not found");
            }
            else if (encounterExecution.Encounter.EncounterType == Domain.EncounterType.Misc)
            {
                encounterExecution.Complete(MapToDomain(encounterExecutionDto));
            }
            //Dodaj Xp Touristi koji je resio izazov
            if (encounterExecution.IsComplete)
            {
                _internalTouristService.AddTouristXp(encounterExecutionDto.UserId, encounter.ExperiencePoints);
            }
            _encounterExecutionRepository.Update(encounterExecution);
            return MapToDto(encounterExecution);
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

        public Result<EncounterExecutionDto> GetByUser(long userId)
        {
            var encounterExecution = _encounterExecutionRepository.GetByUserId(userId);
            if (encounterExecution is null) return Result.Fail("There is no encounter executions for this user");
            if(!encounterExecution.IsComplete)
                return MapToDto(encounterExecution);
            return Result.Fail("There is no active executions for this user");
        }

        public Result<EncounterExecutionDto> FindExecution(long userId, long encounterId)
        { 
            var allExecutions = _encounterExecutionRepository.GetAll();
            var execution = allExecutions.FirstOrDefault(e => e.UserId == userId && e.EncounterId == encounterId);
            if (execution == null)
            {
                return Result.Ok<EncounterExecutionDto>(null);
            }

            return MapToDto(execution);
        }

        public Result<List<long>> GetAllEncountersIdsByUserId(long userId)
        {
            var encounterExecutionIds = _encounterExecutionRepository.GetAllEncounterIdsByUserId(userId);
            if (encounterExecutionIds is null) return Result.Fail("Not found");
            return encounterExecutionIds;
        }
    }
}
