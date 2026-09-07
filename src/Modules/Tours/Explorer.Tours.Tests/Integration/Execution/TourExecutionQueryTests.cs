using Explorer.API.Controllers.Authoring.Tour;
using Explorer.API.Controllers.Tourist.Execution;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Execution
{
    [Collection("Sequential")]
    public class TourExecutionQueryTests : BaseToursIntegrationTest
    {
        public TourExecutionQueryTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void Retrieves_one(int id)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetById(id).Result)?.Value as TourExecutionDto;
            // Assert
            result.ShouldNotBeNull();
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
