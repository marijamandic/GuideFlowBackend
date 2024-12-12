using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class AccountCommandTests: BaseStakeholdersIntegrationTest
    {
        public AccountCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Toggles_Account_Activity()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var accountNum1 = new AccountOverviewDto
            {
                Id = -12,
                Username = "autor2@gmail.com",
                Email = "autor2@gmail.com ",
                Role = UserRole.Author,
                IsActive = true

            };
            var accountNum2 = new AccountOverviewDto
            {
                Id = -13,
                Username = "autor3@gmail.com",
                Email = "autor3@gmail.com ",
                Role = UserRole.Author,
                IsActive = false
            };



            var result = ((ObjectResult)controller.ToggleAccountActivity(accountNum1).Result)?.Value as AccountOverviewDto;
            var result2 = ((ObjectResult)controller.ToggleAccountActivity(accountNum2).Result)?.Value as AccountOverviewDto;



            var storedEntity = dbContext.Users.FirstOrDefault(user => user.Id == accountNum1.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(accountNum1.Id);
            storedEntity.IsActive.ShouldBeFalse();
            result.ShouldNotBeNull();
            result.IsActive.ShouldBeFalse();

            var storedEntity2 = dbContext.Users.FirstOrDefault(user => user.Id == accountNum2.Id);
            storedEntity2.ShouldNotBeNull();
            storedEntity2.Id.ShouldBe(accountNum2.Id);
            storedEntity2.IsActive.ShouldBeTrue();
            result2.ShouldNotBeNull();
            result2.IsActive.ShouldBeTrue();
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
