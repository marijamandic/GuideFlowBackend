using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public.Shopping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Shopping;

[Collection("Sequential")]
public class PurchaseTokenQueryTests : BaseToursIntegrationTest
{
    public PurchaseTokenQueryTests(ToursTestFactory factory) : base(factory) { }
    /*
    [Fact]
    public void Retrieves_all_purchase_tokens()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetPaged(1, 10).Result)?.Value as PagedResult<PurchaseTokenDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.ShouldNotBeEmpty(); // Proverava da li lista sadrži rezultate
        result.TotalCount.ShouldBeGreaterThan(0); // Proverava da li je TotalCount veći od nule
    }*/

    private static PurchaseTokenController CreateController(IServiceScope scope)
    {
        return new PurchaseTokenController(scope.ServiceProvider.GetRequiredService<IPurchaseTokenService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
