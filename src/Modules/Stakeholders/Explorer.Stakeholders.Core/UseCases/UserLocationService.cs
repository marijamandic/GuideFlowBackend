using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserLocationService: IUserLocationService
    {
        private readonly IUserLocationRepository userLocationRepository;
        private readonly IMapper _mapper;

        public UserLocationService(IMapper mapper, IUserLocationRepository userLocationRepository)
        {
            this.userLocationRepository = userLocationRepository;
            _mapper = mapper;
        }

        // Generička metoda za mapiranje domen objekta u DTO
        protected TDto MapToDto<TDomain, TDto>(TDomain result)
        {
            return _mapper.Map<TDto>(result);
        }

        // Metoda koja koristi generičku MapToDto
        public Result<UserLocationDto> GetById(int id)
        {
            UserLocation userLocation = userLocationRepository.GetById(id);
            UserLocationDto userLocationDto = MapToDto<UserLocation, UserLocationDto>(userLocation);
            return Result.Ok(userLocationDto);  // FluentResults
        }

        public Result<UserLocationDto> UpdateLocation(UserLocationDto userLocation)
        {
            throw new NotImplementedException();
        }

        public Result<UserLocationDto> AddUserLocation(UserLocationDto userLocation)
        {
            throw new NotImplementedException();
        }
    }

}
