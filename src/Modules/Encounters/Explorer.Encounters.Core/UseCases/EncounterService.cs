using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : BaseService<EncounterDto,Encounter> , IEncounterService
    {
        private readonly IEncountersRepository _encountersRepository;
        public EncounterService(IEncountersRepository encountersRepository , IMapper mapper) : base(mapper)
        {
            _encountersRepository = encountersRepository;
        }
        public Result<EncounterDto> Get(long id) {
            var encounter = _encountersRepository.Get(id);
            if (encounter is null)
                return Result.Fail(FailureCode.NotFound);
            return MapToDto(encounter);
        }
        public Result<EncounterDto> Create(EncounterDto encounterDto) {
        
            var encounter = _encountersRepository.Create(MapToDomain(encounterDto));
            return MapToDto(encounter);
        }
        public Result<EncounterDto> Update(EncounterDto encounterDto) {
            var encounter = _encountersRepository.Update(MapToDomain(encounterDto)); 
            return MapToDto(encounter);
        }
        public Result<PagedResult<EncounterDto>> GetPaged(int page, int pageSize)
        {
            var encounters = _encountersRepository.GetPaged(page, pageSize);
            return MapToDto(encounters);
        }
        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Result<PagedResult<EncounterDto>> SearchAndFilter(string? name, int? type)
        {
            var encounters = _encountersRepository.SearchAndFilter(name, type);
            return MapToDto(encounters);
        }
    }
}
