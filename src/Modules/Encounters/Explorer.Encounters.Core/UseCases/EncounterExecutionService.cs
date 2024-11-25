using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos.Execution;
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
        private readonly IUserRepository _userRepository;
        public EncounterExecutionService(IEncounterExecutionRepository encounterExecutionRepository, IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _encounterExecutionRepository = encounterExecutionRepository;
            _userRepository = userRepository;
        }
        public Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterExecutionDto, int userId)
        {
            var allExecution = _encounterExecutionRepository.GetAll();
            var execution = MapToDomain(encounterExecutionDto);
            var user = _userRepository.Get(userId);

            //da li je korisnik u blizini aktivira taj izazov ili da mu se pridruzi
            if (execution.IsTouristNear(user.Location.Longitude, user.Location.Latitude))
            {
                //ako postoji social enc baci ga na join
                if (allExecution.Contains(execution) && encounterExecutionDto.EncounterType.Equals(Domain.EncounterType.Social) && !encounterExecutionDto.IsComplete)
                {
                    Join(user, encounterExecutionDto);
                }
                else if (!allExecution.Contains(execution)) // ako ne postoji onda se pravi nova ex
                {
                    _encounterExecutionRepository.Create(execution);
                    return MapToDto(execution);
                }
                return Result.Fail("Execution not created, already exists");
            }

            return Result.Fail("Tourist can't activate or join this encounter");
        }

        private void Join(User user, EncounterExecutionDto encounterExecutionDto)
        {
            //doda se user u execution 
            encounterExecutionDto.touristsIncluded.Add(user);
            var execution = MapToDomain(encounterExecutionDto);

            // prebaci se isCompleted na true ako ima dovoljno ljudi i onda se uradi update
            execution.CompleteSocialEncounter();
            Update(MapToDto(execution));

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

        public Result<EncounterExecutionDto> Complete(EncounterExecutionDto encounterExecutionDto)
        {
            var execution = MapToDomain(encounterExecutionDto);
            execution.Complete();
            return MapToDto(execution);
        }
    }
}
