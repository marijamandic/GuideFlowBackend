using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class ReviewsCommandTests : BaseToursIntegrationTest
{
    public ReviewsCommandTests(ToursTestFactory factory) : base(factory) { }

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
            Comment = "probni",
            TourDate = DateTime.UtcNow,
            CreationDate = DateTime.UtcNow

        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Rating.ShouldBe(newEntity.Rating);
        result.Comment.ShouldBe(newEntity.Comment);
        result.TourDate.ShouldBe(newEntity.TourDate);
        result.CreationDate.ShouldBe(newEntity.CreationDate);

        // Assert - Database
        var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Comment == newEntity.Comment);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    private static TourReviewController CreateController(IServiceScope scope)
    {
        return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
