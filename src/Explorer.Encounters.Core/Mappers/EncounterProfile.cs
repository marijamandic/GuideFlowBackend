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
            CreateMap<EncounterDto, Encounter>()
           .ForMember(dest => dest.EncounterLocation, opt => opt.MapFrom(src => src.EncounterLocationDto)).ReverseMap();
            CreateMap<EncounterLocationDto, EncounterLocation>().ReverseMap();
        }
    }
}
