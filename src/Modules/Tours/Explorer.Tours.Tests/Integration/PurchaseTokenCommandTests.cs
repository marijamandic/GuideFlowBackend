using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Shopping;

[Collection("Sequential")]
public class PurchaseTokenCommandTests : BaseToursIntegrationTest
{
    public PurchaseTokenCommandTests(ToursTestFactory factory) : base(factory) { }

    /*
    [Fact]
    public void Creates_purchase_token()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new PurchaseTokenDto
        {
            UserId = 1,
            TourId = 100 // Assuming 100 is a valid tourId
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PurchaseTokenDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0); // Ensure that ID is generated
        result.UserId.ShouldBe(newEntity.UserId);
        result.TourId.ShouldBe(newEntity.TourId);

        // Assert - Database
        var storedEntity = dbContext.PurchaseTokens.FirstOrDefault(i => i.UserId == newEntity.UserId && i.TourId == newEntity.TourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new PurchaseTokenDto
        {
            UserId = 0,  // Invalid UserId
            TourId = 0   // Invalid TourId
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400); // Bad request due to invalid data
    }

    [Fact]
    public void Updates_purchase_token()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new PurchaseTokenDto
        {
            Id = -1, // Existing ID
            UserId = 1,
            TourId = 200 // New TourId
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity.Id, updatedEntity).Result)?.Value as PurchaseTokenDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(updatedEntity.Id);
        result.TourId.ShouldBe(updatedEntity.TourId);

        // Assert - Database
        var storedEntity = dbContext.PurchaseTokens.FirstOrDefault(i => i.Id == updatedEntity.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.TourId.ShouldBe(updatedEntity.TourId);
    }

    [Fact]
    public void Deletes_purchase_token()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(-3); // Assuming -3 exists

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200); // Successful deletion

        // Assert - Database
        var storedEntity = dbContext.PurchaseTokens.FirstOrDefault(i => i.Id == -3);
        storedEntity.ShouldBeNull(); // Entity should be deleted
    }
    */
    private static PurchaseTokenController CreateController(IServiceScope scope)
    {
        return new PurchaseTokenController(scope.ServiceProvider.GetRequiredService<IPurchaseTokenService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
