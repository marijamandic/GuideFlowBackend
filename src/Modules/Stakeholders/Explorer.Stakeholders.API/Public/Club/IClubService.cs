using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public.Club
{
    public interface IClubService
    {
        Result<PagedResult<ClubDto>> GetPaged(int page, int pageSize);
        Result<ClubDto> Get(int id);
        Result<ClubDto> Create(ClubDto club);
        Result<ClubDto> Update(ClubDto club);
        Result Delete(int id);
    }
}
