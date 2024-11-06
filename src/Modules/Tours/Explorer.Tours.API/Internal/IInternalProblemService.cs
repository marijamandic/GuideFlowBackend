using FluentResults;

namespace Explorer.Tours.API.Internal;

public interface IInternalProblemService
{
    Result<List<long>> GetTourIdsByAuthorId(int authorId);
    Result<int> GetAuthorIdByTourId(long tourId);
}
