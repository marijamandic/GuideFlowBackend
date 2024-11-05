using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;
public interface IProblemService
{
    Result<ProblemDto> Create(CreateProblemInputDto problemInput);

    /// <summary>
    /// Gets all problems with all value objects and related entitites.
    /// Using .Include method from EF
    /// </summary>
    /// <returns></returns>
    Result<PagedResult<ProblemDto>> GetAll();

    /// <summary>
    /// Gets all problems by authorId, with all value objects and related entitites.
    /// </summary>
    /// <param name="authorId"></param>
    /// <returns></returns>
    Result<PagedResult<ProblemDto>> GetByAuthorId(int authorId);
    Result<PagedResult<MessageDto>> CreateMessage(int userId, CreateMessageInputDto messageInput);
    Result<PagedResult<ProblemDto>> GetByTouristId(int touristId);
}
