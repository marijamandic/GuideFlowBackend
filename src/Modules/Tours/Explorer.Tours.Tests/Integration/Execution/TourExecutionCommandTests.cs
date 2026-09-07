using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExecutionStatus = Explorer.Tours.Core.Domain.TourExecutions.ExecutionStatus;


namespace Explorer.Tours.Tests.Integration.Execution
{
    public class TourExecutionCommandTests : BaseToursIntegrationTest
    {
        public TourExecutionCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new CreateTourExecutionDto
            {
                TourId = -3,
                UserId = 4
            };


            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourExecutionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(newEntity.UserId);

            // Assert - Database
            var storedEntity = dbContext.TourExecutions.FirstOrDefault(i => i.TourId == newEntity.TourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CreateTourExecutionDto
            {
                UserId = 15,
                TourId = -100,
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Theory]
        [InlineData(1, 200, ExecutionStatus.Completed)]
        [InlineData(2, 500, ExecutionStatus.Active)]
        public void CompleteSession(int userId, int expectedStatusCode, ExecutionStatus expectedStatus)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.CompleteSession(userId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatusCode);

            // Assert - Database
            if (result.StatusCode == expectedStatusCode && expectedStatusCode == 200)
            {
                var storedEntity = dbContext.TourExecutions.FirstOrDefault(t => t.UserId == userId);
                storedEntity.ShouldNotBeNull();
                storedEntity.ExecutionStatus.ShouldBe(expectedStatus);
            }
        }

        [Theory]
        [InlineData(3, 200, ExecutionStatus.Abandoned)]
        [InlineData(4, 500, ExecutionStatus.Completed)]
        public void AbandonSession(int userId, int expectedStatusCode, ExecutionStatus expectedStatus)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.AbandonSession(userId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatusCode);

            // Assert - Database
            if (result.StatusCode == expectedStatusCode && expectedStatusCode == 200)
            {
                var storedEntity = dbContext.TourExecutions.FirstOrDefault(t => t.UserId == userId);
                storedEntity.ShouldNotBeNull();
                storedEntity.ExecutionStatus.ShouldBe(expectedStatus);
            }
        }


        private static TourExecutionController CreateController(IServiceScope scope)
        {
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
