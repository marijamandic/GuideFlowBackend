using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
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
        public EncounterService(ICrudRepository<Encounter> repository ,IEncountersRepository encountersRepository , IMapper mapper) : base(repository , mapper)
        {
            _encountersRepository = encountersRepository;
        }
    }
}
