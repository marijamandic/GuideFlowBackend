using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class AccountQueryTests: BaseStakeholdersIntegrationTest
    {
        public AccountQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Gets_All_Accounts()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);


            var result = ((ObjectResult)controller.GetAllAccounts().Result)?.Value as List<AccountOverviewDto>;


            result.ShouldNotBeNull();
            result.Count.ShouldBe(8);
        }

        [Fact]
        public void Gets_Account()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newAccount = new AccountOverviewDto
            {
                UserId = -12,
                Username = "autor2@gmail.com",
                Email = "autor2@gmail.com ",
                Role = UserRole.Author,
                IsActive = true

            };
            var adminUserId = -1;



            var result = ((ObjectResult)controller.GetAccount(newAccount.UserId).Result)?.Value as AccountOverviewDto;
            var result2 = ((ObjectResult)controller.GetAccount(adminUserId).Result)?.Value as AccountOverviewDto;



            result.ShouldNotBeNull();
            result.IsActive.ShouldBeTrue();
            result.UserId.ShouldBe(newAccount.UserId);
            result2.ShouldBeNull();

        }

        private static AccountController CreateController(IServiceScope scope)
        {
            return new AccountController(scope.ServiceProvider.GetRequiredService<IAccountService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
