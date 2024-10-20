using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    [Collection("Sequential")]
    public class ClubRequestQueryTests : BaseStakeholdersIntegrationTest
    {
        public ClubRequestQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetRequest(3).Result)?.Value as PagedResult<ClubRequestDto>;
         
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);
        }

        private static ClubRequestController CreateController(IServiceScope scope)
        {
            return new ClubRequestController(scope.ServiceProvider.GetRequiredService<IClubRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
