using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Author;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Execution;

public class InternalProblemService : IInternalProblemService
{
    private readonly ITourService _tourService;

    public InternalProblemService(ITourService tourService)
    {
        _tourService = tourService;
    }

    public Result<int> GetAuthorIdByTourId(long tourId)
    {
        try
        {
            var result = _tourService.Get((int)tourId).Value;
            return result.AuthorId;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<List<long>> GetTourIdsByAuthorId(int authorId)
    {
        try
        {
            return _tourService.GetTourIdsByAuthorId(authorId);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
