using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using global::Explorer.API.Controllers.Tourist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
namespace Explorer.Tours.Tests.Integration
{

    [Collection("Sequential")]
    public class TourSpecificationCommandTests : BaseToursIntegrationTest
    {
        public TourSpecificationCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void CreatesTourSpecification()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourSpecificationDto
            {
                UserId = 1001,
                TourDifficulty = 2,
                WalkRating = 2,
                BikeRating = 1,
                CarRating = 0,
                BoatRating = 0,
                Tags = new List<string> { "Adventure", "Mountain" }
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourSpecificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(1001);
            result.TourDifficulty.ShouldBe(newEntity.TourDifficulty);

            // Assert - Database
            var storedEntity = dbContext.TourSpecifications.FirstOrDefault(i => i.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.UserId.ShouldBe(newEntity.UserId);
        }

        [Fact]
        public void UpdateTourSpecification()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourSpecificationDto
            {
                UserId = 1001,
                TourDifficulty = 4,
                WalkRating = 2,
                BikeRating = 1,
                CarRating = 0,
                BoatRating = 0,
                Tags = new List<string> { "Adventure", "Desert" }
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity.UserId, updatedEntity).Result)?.Value as TourSpecificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(1001);
            result.TourDifficulty.ShouldBe(updatedEntity.TourDifficulty);

            // Assert - Database
            var storedEntity = dbContext.TourSpecifications.FirstOrDefault(i => i.UserId == updatedEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.TourDifficulty.ShouldBe(updatedEntity.TourDifficulty);
        }

        [Fact]
        public void DeleteTourSpecification()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(1001);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.TourSpecifications.FirstOrDefault(i => i.UserId == 1001);
            storedEntity.ShouldBeNull();
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
