// PRIVREMENO RESENJE

using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal;

public interface IInternalTourHelperService
{
	Result<TourDto> Get(int id);
}
