using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourSpecificationQueryTests : BaseToursIntegrationTest
    {
        public TourSpecificationQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void RetrievesAllTourSpecifications()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as List<TourSpecificationDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0); // Assuming there are some entries in the database.
        }

        private static TourSpecificationController CreateController(IServiceScope scope)
        {
            return new TourSpecificationController(scope.ServiceProvider.GetRequiredService<ITourSpecificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
