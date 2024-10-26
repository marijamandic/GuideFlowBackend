namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    bool Exists(string username);
    User GetById(long id);
    User? GetActiveByName(string username);
    List<User> GetAll();
    User Update(User user);
    User Create(User user);
    long GetPersonId(long userId);
}