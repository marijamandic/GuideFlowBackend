//using Explorer.API.Controllers.Tourist.Shopping;
//using Explorer.Payments.API.Dtos.ShoppingCarts;
//using Explorer.Payments.API.Public;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Shouldly;

//namespace Explorer.Payments.Tests.Integration.Shopping;

//public class ShoppingCartQueryTests : BasePaymentsIntegrationTests
//{
//    public ShoppingCartQueryTests(PaymentsTestFactory factory) : base(factory) { }

//    [Fact]
//    public void GetByTouristId_ReturnsExpectedCart()
//    {
//        // Arrange
//        using var scope = Factory.Services.CreateScope();
//        var controller = CreateShoppingCartController(scope);

//        // Act
//        var result = (controller.GetByTouristId().Result as ObjectResult)!.Value as ShoppingCartDto;

//        // Assert
//        result.ShouldNotBeNull();
//        result.Items.Count.ShouldBeGreaterThanOrEqualTo(2);
//    }

//    private static ShoppingCartController CreateShoppingCartController(IServiceScope scope)
//    {
//        return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
//        {
//            ControllerContext = BuildContext("-21")
//        };
//    }
//}
