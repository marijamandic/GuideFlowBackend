using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubPostService
    {
        Result<ClubPostDto> Create(ClubPostDto clubPostDto);
        Result<ClubPostDto> Update(ClubPostDto clubPostDto);
        Result Delete(int id);
        Result<List<ClubPostDto>> GetAll();
        Result<PagedResult<ClubPostDto>> GetPaged(int pageIndex, int pageSize);
        Result<ClubPostDto> GetByClubId(long clubId);
    }
}
