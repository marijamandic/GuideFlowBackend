using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
public interface IProblemRepository
{
    Problem Create(Problem problem);
    PagedResult<Problem> GetPaged(int page, int pageSize);
}
