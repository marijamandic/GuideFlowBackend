using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class CheckpointQueryTests : BaseToursIntegrationTest
{
    public CheckpointQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_checkpoints()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as IEnumerable<CheckpointDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(3); // Očekivani broj checkpoint-ova u bazi (menjaj po potrebi)
    }

    private static CheckpointController CreateController(IServiceScope scope)
    {
        return new CheckpointController(scope.ServiceProvider.GetRequiredService<ICheckpointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
