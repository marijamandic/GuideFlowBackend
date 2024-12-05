using FluentResults;

namespace Explorer.Payments.API.Internal;

public interface IInternalTourService
{
	Task<Result<List<int>>> GetTourIdsByTouristId(int touristId);
}
