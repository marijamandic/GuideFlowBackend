using AutoMapper;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class ProfileInfoProfile : Profile
    {
        public ProfileInfoProfile()
        {
            CreateMap<ProfileInfoDto, ProfileInfo>().ReverseMap();
        }
    }
}
