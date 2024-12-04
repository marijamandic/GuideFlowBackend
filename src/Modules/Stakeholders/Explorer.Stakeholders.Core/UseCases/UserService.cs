using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
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
    public class UserService : CrudService<UserDto, User>, IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            this.userRepository = userRepository;
        }

        public Result<UserDto> GetById(int id)
        {
            User user = userRepository.GetById(id);
            return MapToDto(user);
        }

        public Result<List<UserDto>> GetAllUsers()
        {
            var users = userRepository.GetAll();
            var userDtos = users.Select(MapToDto).ToList();
            return Result.Ok(userDtos);
        }
    }
}
