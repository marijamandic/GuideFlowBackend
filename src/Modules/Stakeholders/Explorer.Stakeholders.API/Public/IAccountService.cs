using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAccountService
    {
        Result<AccountOverviewDto> GetAccount(long id);

        Result<List<AccountOverviewDto>> GetAllAccounts();

        Result<AccountOverviewDto> ToggleAccountActivity(AccountOverviewDto account);
    }
}
