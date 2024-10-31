using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
public interface IProblemRepository
{
    PagedResult<Problem> GetAll();
}
