using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncounterProfile : Profile
    {
        public EncounterProfile()
        {
            CreateMap<EncounterDto, Encounter>().ReverseMap();
            CreateMap<EncounterLocationDto, EncounterLocation>().ReverseMap();
            CreateMap<MiscEncounterDto, MiscEncounter>().ReverseMap();
            CreateMap<SocialEncounterDto , SocialEncounter>().ReverseMap();
            CreateMap<HiddenLocationEncounterDto , HiddenLocationEncounter>().ReverseMap();
        }
    }
}
