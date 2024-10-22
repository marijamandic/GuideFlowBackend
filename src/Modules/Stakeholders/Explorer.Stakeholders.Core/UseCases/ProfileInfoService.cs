using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
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
        public ProfileInfoService(ICrudRepository<ProfileInfo> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<List<ProfileInfoDto>> GetAll()
        {
            var pagedResult = GetPaged(1, int.MaxValue);

            if (pagedResult.IsFailed)
            {
                return Result.Fail<List<ProfileInfoDto>>(pagedResult.Errors);
            }

            return Result.Ok(pagedResult.Value.Results);
        }

        public Result<PagedResult<ProfileInfoDto>> GetPaged(int pageIndex, int pageSize)
        {
            return base.GetPaged(pageIndex, pageSize);
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
