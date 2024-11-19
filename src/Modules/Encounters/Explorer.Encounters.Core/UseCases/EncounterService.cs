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
using EncounterType = Explorer.Encounters.API.Dtos.EncounterType;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterDto,Encounter> , IEncounterService
    {
        private readonly IEncountersRepository _encountersRepository;
        private readonly IMapper _mapper;
        public EncounterService(ICrudRepository<Encounter> repository ,IEncountersRepository encountersRepository , IMapper mapper) : base(repository , mapper)
        {
            _encountersRepository = encountersRepository;
            _mapper = mapper;
        }

        public Result<EncounterDto> Get(EncounterType type, long id) {
            if (type == EncounterType.Misc)
            {
                var encounter = _encountersRepository.GetMisc(id);
                if (encounter is null)
                    return Result.Fail(FailureCode.NotFound);
                return _mapper.Map<MiscEncounterDto>(encounter);
            }
            else if (type == EncounterType.Social)
            {
                var encounter = _encountersRepository.GetSocial(id);
                if (encounter is null)
                    return Result.Fail(FailureCode.NotFound);
                return _mapper.Map<SocialEncounterDto>(encounter);
            }
            else if (type == EncounterType.Location) { 
                var encounter = _encountersRepository.GetLocation(id);
                if (encounter is null)
                    return Result.Fail(FailureCode.NotFound);
                return _mapper.Map<HiddenLocationEncounterDto>(encounter);
            }
            return Result.Fail(FailureCode.InvalidArgument);
        }
        public new Result<EncounterDto> Create(EncounterDto encounterDto) {
            var encounter = MapToEncounterType(encounterDto);
            if (encounter is null)
                return Result.Fail(FailureCode.InvalidArgument);
            _encountersRepository.Create(encounter);

            return MapToDto(encounter);
        }
        public new Result<EncounterDto> Update(EncounterDto encounterDto) {
            var encounter = MapToEncounterType(encounterDto);
            if (encounter is null)
                return Result.Fail(FailureCode.InvalidArgument);
            _encountersRepository.Update(encounter); 
            return MapToDto(encounter);
        }

        #region HelpperMethods
        private Encounter MapToEncounterType(EncounterDto encounterDto)
        {
            if (encounterDto is SocialEncounterDto socialEncounterDto)
            {
                return _mapper.Map<SocialEncounter>(socialEncounterDto);
            }
            else if (encounterDto is HiddenLocationEncounterDto locationEncounterDto)
            {
                return _mapper.Map<HiddenLocationEncounter>(locationEncounterDto);
            }
            else if (encounterDto is MiscEncounterDto miscEncounterDto)
            {
                return _mapper.Map<MiscEncounter>(miscEncounterDto);
            }
            return null;
        }
        #endregion
    }
}
