using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public  Location Location { get; private set; }

    public DateTime LastLogin { get; private set; }
    public DateTime LastLogout { get; private set; }
    protected User()
    {
    }
    public User(string username, string password, UserRole role, bool isActive, Location location)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Validate();
        Location = location;
    }

    public void SetLocation(Location location)
    {
        Location = location;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }

    public void SetLastLoginTime()
    {
        LastLogin = DateTime.UtcNow;
    }
    public void SetLastLogoutTime()
    {
        LastLogout = DateTime.UtcNow;
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}