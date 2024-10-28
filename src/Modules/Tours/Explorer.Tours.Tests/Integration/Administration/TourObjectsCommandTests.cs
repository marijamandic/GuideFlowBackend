using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class TourObjectsCommandTests : BaseToursIntegrationTest
{
    public TourObjectsCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourObjectDto
        {
            Name = "McDonalds",
            Description = "Super hrana",
            ImageUrl = "slika",
            Category = ObjectCategory.Restaurant,
            Latitude = 10,
            Longitude = 20,
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourObjectDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.TourObjects.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourObjectDto
        {
            Description = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    private static TourObjectController CreateController(IServiceScope scope)
    {
        return new TourObjectController(scope.ServiceProvider.GetRequiredService<ITourObjectService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}