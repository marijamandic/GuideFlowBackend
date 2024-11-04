using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
public interface IProblemRepository
{
    Problem Create(Problem problem);
    PagedResult<Problem> GetAll();
    Problem Update(Problem problem);
    Problem GetById(int id);
}
