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
        Result<PagedResult<ProfileInfoDto>> GetPaged(int page, int pageSize);
        Result<ProfileInfoDto> GetByUserId(int id);
        Result<ProfileInfoDto> Create(ProfileInfoDto profileInfoDto);
        Result<ProfileInfoDto> Update(ProfileInfoDto profileInfoDto);
        Result<ProfileInfoDto> UpdateFollowers(FollowerDto follower);
        public Result<List<ProfileInfoDto>> GetAllUsers();
        Result<List<int>> GetFollowers(int userId);
        Result Delete(int id);
        Result<List<int>> GetFollowed(int userId);
    }
}
