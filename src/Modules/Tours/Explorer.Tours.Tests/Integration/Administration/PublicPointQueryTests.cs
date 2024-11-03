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
        var result = controller.GetAll(1, 10); 

        // Assert
        result.ShouldNotBeNull();
        var publicPointsResult = result.Result as OkObjectResult; 
        publicPointsResult.ShouldNotBeNull(); 

        var publicPoints = publicPointsResult.Value as IEnumerable<PublicPointDto>;
        publicPoints.ShouldNotBeNull(); 

        publicPoints.Count().ShouldBe(2);
    }

    private static PublicPointController CreateController(IServiceScope scope)
    {
        return new PublicPointController(scope.ServiceProvider.GetRequiredService<IPublicPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
