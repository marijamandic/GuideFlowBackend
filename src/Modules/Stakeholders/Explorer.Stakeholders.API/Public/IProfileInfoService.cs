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
        Result<ProfileInfoDto> Create(ProfileInfoDto profileInfoDto);
        Result<ProfileInfoDto> Update(ProfileInfoDto profileInfoDto);
        Result Delete(int id);
        Result<List<ProfileInfoDto>> GetAll();
        Result<PagedResult<ProfileInfoDto>> GetPaged(int pageIndex, int pageSize);
    }
}
