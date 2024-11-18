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
    public class EncounterService : CrudService<EncounterDto,Encounter> , IEncounterService
    {
        private readonly IEncountersRepository _encountersRepository;
        private readonly IMapper _mapper;
        public EncounterService(ICrudRepository<Encounter> repository ,IEncountersRepository encountersRepository , IMapper mapper) : base(repository , mapper)
        {
            _encountersRepository = encountersRepository;
            _mapper = mapper;
        }

        public Result<EncounterDto> Get(string type, long id) {
            if (type.Equals("misc"))
            {
                var encounter = _encountersRepository.GetMisc(id);
                return _mapper.Map<MiscEncounterDto>(encounter);
            }
            else if (type.Equals("social"))
            {
                var counter = _encountersRepository.GetSocial(id);
                return _mapper.Map<SocialEncounterDto>(counter);
            }
            else if (type.Equals("location")) { 
                var counter = _encountersRepository.GetLocation(id);
                return _mapper.Map<LocationEncounterDto>(counter);
            }
            return Result.Fail(FailureCode.InvalidArgument);
        }
        public new Result<EncounterDto> Create(EncounterDto encounterDto) {
            Encounter encounter;
            if (encounterDto is SocialEncounterDto socialEncounterDto)
            {
                encounter = _mapper.Map<SocialEncounter>(socialEncounterDto);
            }
            else if (encounterDto is LocationEncounterDto locationEncounterDto)
            {
                encounter = _mapper.Map<LocationEncounter>(locationEncounterDto);
            }
            else if (encounterDto is MiscEncounterDto miscEncounterDto)
            {
                encounter = _mapper.Map<MiscEncounter>(miscEncounterDto);
            }
            else {
                return Result.Fail(FailureCode.InvalidArgument);
            }
            _encountersRepository.Create(encounter);

            return MapToDto(encounter);
        }
        public new Result<EncounterDto> Update(EncounterDto encounterDto) {
            Encounter encounter;
            if (encounterDto is SocialEncounterDto socialEncounterDto)
            {
                encounter = _mapper.Map<SocialEncounter>(socialEncounterDto);
            }
            else if (encounterDto is LocationEncounterDto locationEncounterDto)
            {
                encounter = _mapper.Map<LocationEncounter>(locationEncounterDto);
            }
            else if (encounterDto is MiscEncounterDto miscEncounterDto)
            {
                encounter = _mapper.Map<MiscEncounter>(miscEncounterDto);
            }
            else
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
            _encountersRepository.Update(encounter); 
            return MapToDto(encounter);
        }
    }
}
