
namespace Explorer.Stakeholders.API.Dtos
{
    public class AccountOverviewDto
    {
        public long id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastLogin { get; set; }
        public DateTime LastLogout { get; set; }
    }

    public enum UserRole
    {
        Administrator,
        Author,
        Tourist
    }
}
