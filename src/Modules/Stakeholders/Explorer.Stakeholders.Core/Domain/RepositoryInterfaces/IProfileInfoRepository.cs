using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IProfileInfoRepository : ICrudRepository<ProfileInfo>
{
    bool Exists(string username);
    ProfileInfo GetByUserId(long id);
    List<ProfileInfo> GetAll();
    long GetPersonId(long userId);
}