using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileInfoService : CrudService<ProfileInfoDto, ProfileInfo>, IProfileInfoService
    {
        private readonly IProfileInfoRepository profileInfoRepository;
        private readonly IMapper _mapper;
        public ProfileInfoService(IProfileInfoRepository profileInfoRepository, IMapper mapper) : base(profileInfoRepository, mapper)
        {
            this.profileInfoRepository = profileInfoRepository;
            _mapper = mapper;
        }

        public Result<ProfileInfoDto> GetByUserId(int id)
        {
            ProfileInfo profileInfo = profileInfoRepository.GetByUserId(id);
            return MapToDto(profileInfo);
        }

        public Result<List<ProfileInfoDto>> GetAllUsers()
        {
            var profiles = profileInfoRepository.GetAll();
            var profileInfoDtos = profiles.Select(MapToDto).ToList();
            return Result.Ok(profileInfoDtos);
        }
        public Result<ProfileInfoDto> UpdateFollowers(FollowerDto follower) {
            var profile = profileInfoRepository.GetByUserId(follower.UserId);
            profile.UpdateFollower(_mapper.Map<Follower>(follower));
            profileInfoRepository.Update(profile);
            return MapToDto(profile);
        }

        public Result<List<int>> GetFollowers(int userId)
        {
            var ids = profileInfoRepository.GetFollowerIdsByUserId(userId);
            return ids;
        }


        /* public ProfileInfoService(ICrudRepository<ProfileInfo> crudRepository, IMapper mapper) : base(crudRepository, mapper)
            {
                _profiles = new List<ProfileInfoDto>();
            }
        public Result<ProfileInfoDto> CreateProfileInfo(ProfileInfoDto profileInfoDto)
        {
            var existingProfileInfo = _profiles.FirstOrDefault(p => p.Id == profileInfoDto.Id);
            if (existingProfileInfo != null)
            {
                throw new ArgumentException("ProfileInfo already exists");
            }
            _profiles.Add(profileInfoDto);
            return profileInfoDto;
        }

        public Result UpdateProfileInfo(ProfileInfoDto profileInfoDto)
        {
            var existingProfileInfo = _profiles.FirstOrDefault(p => p.Id == profileInfoDto.Id);

            if (existingProfileInfo != null)
            {
                existingProfileInfo.Id = profileInfoDto.Id;
                existingProfileInfo.UserId = profileInfoDto.UserId;
                existingProfileInfo.FirstName = profileInfoDto.FirstName;
                existingProfileInfo.LastName = profileInfoDto.LastName;
                existingProfileInfo.ProfilePicture = profileInfoDto.ProfilePicture;
                existingProfileInfo.Biography = profileInfoDto.Biography;
                existingProfileInfo.Moto = profileInfoDto.Moto;
                return Result.Ok();
            }

            else
            {
                return Result.Fail("User not found");
            }
        }

        public Result DeleteProfileInfo(long id)
        {
            var existingProfileInfo = _profiles.FirstOrDefault(p => p.Id == id);
            if (existingProfileInfo != null)
            {
                _profiles.Remove(existingProfileInfo);
                return Result.Ok();
            }
            else
            {
                return Result.Fail("ProfileInfo not found");
            }
        }

        public Result<ProfileInfoDto> GetProfileInfoById(long id)
        {
            var existingProfileInfo = _profiles.FirstOrDefault(p => p.Id == id);
            if (existingProfileInfo != null)
            {
                return existingProfileInfo;
            }
            else
            {
                return Result.Fail("ProfileInfo with this id is not found");
            }
        }*/
    }
}
