using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
public interface IProblemRepository
{
    Problem Create(Problem problem);
    PagedResult<Problem> GetAll();
    PagedResult<Problem> GetByTourIds(List<long> tourIds);
    Problem GetById(long id);
    Problem Save(Problem problem);
    PagedResult<Problem> GetByTouristId(long touristId);
}
