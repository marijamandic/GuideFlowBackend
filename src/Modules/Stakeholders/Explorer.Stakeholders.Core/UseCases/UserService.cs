using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : CrudService<UserDto, User>, IUserService, IInternalTouristService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public Result<UserDto> GetById(int id)
        {
            User user = userRepository.GetById(id);
            if (user is null) return Result.Fail("User not found");
            return MapToDto(user);
        }

        public Result<List<UserDto>> GetAllUsers()
        {
            var users = userRepository.GetAll();
            var userDtos = users.Select(MapToDto).ToList();
            return Result.Ok(userDtos);
        }
        public Result<TouristDto> GetTouristById(int id)
        {
            Tourist tourist = userRepository.GetTouristById(id);
            TouristDto touristDto = mapper.Map<TouristDto>(tourist);
            return Result.Ok(touristDto);
        }
        /*public Result<TouristDto> UpdateTourist(TouristDto touristDto)
        {
            Tourist existingTourist = userRepository.GetTouristById(touristDto.Id);
            if (existingTourist == null)
            {
                return Result.Fail("Tourist not found.");
            }

            existingTourist.UpdateXp(touristDto.Xp);
        
            userRepository.UpdateTourist(existingTourist);

            return Result.Ok(mapper.Map<TouristDto>(existingTourist));
        }*/
        public Result<TouristDto> AddTouristXp(int id,int amount)
        {
            Tourist existingTourist = userRepository.GetTouristById(id);
            if (existingTourist == null)
            {
                return Result.Fail("Tourist not found.");
            }
            existingTourist.AddXp(amount);
            userRepository.UpdateTourist(existingTourist);

            return Result.Ok(mapper.Map<TouristDto>(existingTourist));
        }

        public Result<TouristDto> AddTouristMoney(int id, int amount)
        {
            Tourist existingTourist = userRepository.GetTouristById(id);
            if (existingTourist == null)
            {
                return Result.Fail("Tourist not found.");
            }
            existingTourist.AddMoney(amount);
            userRepository.UpdateTourist(existingTourist);

            return Result.Ok(mapper.Map<TouristDto>(existingTourist));
        }

        public Result<TouristDto> CreateTourist(UserDto userDto)
        {
            // Kreirajte novog turistu
            Location location = new Location(userDto.Location.Longitude, userDto.Location.Latitude);
            var tourist = new Tourist(
                username: userDto.Username,
                password: userDto.Password,
                role: Domain.UserRole.Tourist,
                isActive: true,
                location: location,
                wallet: 0,
                xp: 0,
                level: 0
            );

            var savedTourist = userRepository.CreateTourist(tourist);

            var touristDto = new TouristDto
            {
                Id = savedTourist.Id,
                Wallet = savedTourist.Wallet,
                Xp = savedTourist.Xp,
                Level = savedTourist.Level
            };

            return Result.Ok(touristDto);
        }

    }
}
