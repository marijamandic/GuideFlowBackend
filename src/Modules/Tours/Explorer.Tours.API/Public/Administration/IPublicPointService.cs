using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPublicPointService
    {
        Result<PublicPointDto> Create(PublicPointDto publicPoint);
        Result<PublicPointDto> Update(PublicPointDto publicPoint);
        Result Delete(int id);
        Result<PublicPointDto> Get(int id);
        Result<PagedResult<PublicPointDto>> GetPaged(int page, int pageSize);
        Result<IEnumerable<PublicPointDto>> GetPendingPublicPoints();

    }
}
