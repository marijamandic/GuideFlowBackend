using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Execution;

public class InternalProblemService : IInternalProblemService
{
    public Result<int> GetAuthorIdByTourId(long tourId)
    {
        return 1;
    }

    public Result<List<long>> GetTourIdsByAuthorId(int authorId)
    {
        return new List<long> { 1, 2, 3 };
    }
}
