using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using FluentResults;
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
        public void Retrieves_all()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetAll().Result)?.Value as IEnumerable<ClubRequestDto>;
         
            result.ShouldNotBeNull();
            /*result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);*/
            result.Count().ShouldBe(5);
        }

        [Fact]
        public void GetRequest_ReturnsRequestById()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long requestId = 1;

            // Act
            var result = ((ObjectResult)controller.GetRequest(requestId).Result)?.Value as ClubRequestDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(requestId);
        }

        [Fact]
        public void Accept_AcceptsMembershipRequest()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long requestId = 5;

            // Act
            var result = ((ObjectResult)controller.Accept(requestId).Result)?.Value as ClubRequestDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(ClubRequestStatus.ACCEPTED); // Assuming the status changes
        }

        [Fact]
        public void Decline_DeclinesMembershipRequest()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long requestId = 3;

            // Act
            var result = ((ObjectResult)controller.Decline(requestId).Result)?.Value as ClubRequestDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(ClubRequestStatus.DECLINED);
        }

        [Fact]
        public void Cancel_CancelsMembershipRequest()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long requestId = 4;

            // Act
            var result = ((ObjectResult)controller.Cancel(requestId).Result)?.Value as ClubRequestDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(ClubRequestStatus.CANCELLED);
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
