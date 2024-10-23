using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration;

[Collection("Sequential")]

public class TourSpecificationCommandTests : BaseToursIntegrationTest
{
    public TourSpecificationCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]

    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourSpecificationDto
        {
            UserId = 1,
            BikeRating = 3,
            BoatRating = 2,
            CarRating = 1,
            WalkRating = 1,
            TourDifficulty = 4,
            Tags = new List<string> { "ndiwe", "ndioew", "dni"}
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourSpecificationDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.UserId.ShouldNotBe(0);

        // Assert - Database
        var storedEntity = dbContext.TourSpecifications.FirstOrDefault(i => i.UserId == newEntity.UserId);
        storedEntity.ShouldNotBeNull();
        storedEntity.UserId.ShouldBe(result.UserId);
    }

    /*[Fact]

    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(1);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.TourSpecifications.FirstOrDefault(i => i.UserId == 1);
        storedCourse.ShouldBeNull();
    }*/

    private static TourSpecificationController CreateController(IServiceScope scope)
    {
        return new TourSpecificationController(scope.ServiceProvider.GetRequiredService<ITourSpecificationService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}