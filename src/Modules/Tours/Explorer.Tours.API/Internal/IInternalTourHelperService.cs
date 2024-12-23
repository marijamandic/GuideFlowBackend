// PRIVREMENO RESENJE

using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal;

public interface IInternalTourHelperService
{
	Result<TourDto> Get(int id);
	Result<PagedResult<TourDto>> GetByIds(IEnumerable<int> ids);
}
