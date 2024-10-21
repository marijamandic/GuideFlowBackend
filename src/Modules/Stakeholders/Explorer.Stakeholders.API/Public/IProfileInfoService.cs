using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProfileInfoService
    {
        Result<ProfileInfoDto> CreateProfileInfo(ProfileInfoDto profileInfoDto);
        Result UpdateProfileInfo(ProfileInfoDto profileInfoDto);
        Result DeleteProfileInfo(long id);
        Result<ProfileInfoDto> GetProfileInfoById(long profileId);
        Result<PagedResult<ProfileInfoDto>> GetPaged(int page, int pageSize);
    }
}
