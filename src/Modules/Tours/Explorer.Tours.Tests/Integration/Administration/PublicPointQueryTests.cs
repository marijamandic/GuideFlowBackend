using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class PublicPointQueryTests : BaseToursIntegrationTest
{
    public PublicPointQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_public_points()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        //var result = controller.GetAll(1, 10);
        var result = ((ObjectResult)controller.GetAll(1, 10).Result)?.Value as PagedResult<PublicPointDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }

    private static PublicPointController CreateController(IServiceScope scope)
    {
        return new PublicPointController(scope.ServiceProvider.GetRequiredService<IPublicPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
