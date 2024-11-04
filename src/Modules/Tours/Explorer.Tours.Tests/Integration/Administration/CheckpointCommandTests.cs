using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class CheckpointCommandTests : BaseToursIntegrationTest
{
    public CheckpointCommandTests(ToursTestFactory factory) : base(factory) { }
    
    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new CheckpointDto
        {
            Name = "Starting Point",
            Description = "Starting checkpoint for the tour.",
            Latitude = 45.2671,
            Longitude = 19.8335,
            ImageUrl = "/images/start-point.jpg",
            Secret = "Tajna"
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CheckpointDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);
        result.Latitude.ShouldBe(newEntity.Latitude);

        // Assert - Database
        var storedEntity = dbContext.Checkpoint.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var invalidEntity = new CheckpointDto
        {
            // Missing required fields like Name and Latitude
            Description = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Create(invalidEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new CheckpointDto
        {
            Id = -3,
            Name = "Updated Checkpoint",
            Description = "Updated description.",
            Latitude = 5.2700,
            Longitude = 19.8400,
            ImageUrl = "/images/updated-checkpoint.jpg",
            Secret = "Tajnaa"
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CheckpointDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-3);
        result.Name.ShouldBe(updatedEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.Checkpoint.FirstOrDefault(i => i.Id == -3);
        storedEntity.ShouldNotBeNull();
        storedEntity.Description.ShouldBe(updatedEntity.Description);
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var invalidEntity = new CheckpointDto
        {
            Id = -1000, // Invalid Id
            Name = "Updated Checkpoint",
            Description = "Updated description.",
            Latitude = 5.2700,
            Longitude = 19.8400,
            ImageUrl = "/images/updated-checkpoint.jpg",
            Secret = "Tajna"
        };

        // Act
        var result = (ObjectResult)controller.Update(invalidEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(-1);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedEntity = dbContext.Checkpoint.FirstOrDefault(i => i.Id == -1);
        storedEntity.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000); // Invalid Id

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static CheckpointController CreateController(IServiceScope scope)
    {
        return new CheckpointController(scope.ServiceProvider.GetRequiredService<ICheckpointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    } 
}
