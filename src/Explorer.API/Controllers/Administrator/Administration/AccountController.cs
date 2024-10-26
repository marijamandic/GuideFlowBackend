using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/account")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id:long}")]
        public ActionResult<AccountOverviewDto> GetAccount(long id)
        {
            var result = _accountService.GetAccount(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<AccountOverviewDto>> GetAllAccounts()
        {
            var result = _accountService.GetAllAccounts();
            return CreateResponse(result);
        }

        [HttpPatch]
        public ActionResult<AccountOverviewDto> ToggleAccountActivity([FromBody] AccountOverviewDto account)
        {
            var result = _accountService.ToggleAccountActivity(account);
            return CreateResponse(result);
        }
    }
}
