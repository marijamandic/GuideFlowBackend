using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IProblemNotificationRepository
{
    void Create(ProblemNotification notification);
    PagedResult<ProblemNotification> GetByUserId(long userId);
}
