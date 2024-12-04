using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository : ICrudRepository<User>
{
    bool Exists(string username);
    User GetById(long id);
    User? GetActiveByName(string username);
    List<User> GetAll();
    long GetPersonId(long userId);
    Tourist GetTouristById(long id);
    Tourist UpdateTourist(Tourist tourist);
    Tourist CreateTourist(Tourist tourist);
}