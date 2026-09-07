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
    public interface IRatingAppService
    {
        Result<RatingAppDto> Create(RatingAppDto rating);
        Result<PagedResult<RatingAppDto>> GetPaged(int page, int pageSize);
    }
}
