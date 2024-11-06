using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class TourReviewCommandTests : BaseToursIntegrationTest
{
    public TourReviewCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourReviewDto
        {
            Rating = 5,
            Comment = "Great tour!",
            TourDate = DateTime.UtcNow.AddDays(-1),  // Use UtcNow
            CreationDate = DateTime.UtcNow,  // Use UtcNow
            PercentageCompleted = 100,
            TouristId = 1,
            TourId = 1
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Rating.ShouldBe(newEntity.Rating);
        result.Comment.ShouldBe(newEntity.Comment);

        // Assert - Database
        var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == result.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Rating.ShouldBe(result.Rating);
        storedEntity.Comment.ShouldBe(result.Comment);
    }


    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var invalidEntity = new TourReviewDto
        {
            // Missing required fields like Rating
            Comment = "Test comment",
            TourDate = DateTime.Now.AddDays(-1),
            CreationDate = DateTime.Now,
            PercentageCompleted = 100,
            TouristId = 1,
            TourId = 1
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
        var updatedEntity = new TourReviewDto
        {
            Id = -2,
            Rating = 4,
            Comment = "Updated comment",
            TourDate = DateTime.UtcNow.AddDays(-1),  // Use UtcNow
            CreationDate = DateTime.UtcNow.AddDays(-1),
            PercentageCompleted = 80,
            TouristId = 1,
            TourId = 1
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity.Id, updatedEntity).Result)?.Value as TourReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-2);
        result.Rating.ShouldBe(updatedEntity.Rating);

        // Assert - Database
        var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == -2);
        storedEntity.ShouldNotBeNull();
        storedEntity.Comment.ShouldBe(updatedEntity.Comment);
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var invalidEntity = new TourReviewDto
        {
            Id = 1000, // Invalid Id
            Rating = 5,
            Comment = "Updated comment",
            TourDate = DateTime.Now.AddDays(-2),
            CreationDate = DateTime.Now.AddDays(-1),
            PercentageCompleted = 100,
            TouristId = 1,
            TourId = 1
        };

        // Act
        var result = (ObjectResult)controller.Update(invalidEntity.Id, invalidEntity).Result;

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
        var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == -1);
        storedEntity.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(1000); // Invalid Id

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static TourReviewController CreateController(IServiceScope scope)
    {
        return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
