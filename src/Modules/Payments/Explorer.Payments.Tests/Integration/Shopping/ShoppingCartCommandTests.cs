﻿using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCarts;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration.Shopping;

public class ShoppingCartCommandTests : BasePaymentsIntegrationTests
{
    public ShoppingCartCommandTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Adds_To_Cart()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateShoppingCartController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        var item = new ItemInputDto
        {
            Type = ProductType.Tour,
            ProductId = -3,
            ProductName = "Mountain Adventure",
            AdventureCoin = 20
        };

        // Act
        var result = (controller.AddToCart(item).Result as ObjectResult)!.Value as PagedResult<ItemDto>;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBeGreaterThanOrEqualTo(2);

        // Assert - Database
        var storedItem = dbContext.ShoppingCartItems.FirstOrDefault(i => i.ProductId == item.ProductId);
        storedItem.ShouldNotBeNull();
        storedItem.ProductName.ShouldBe(item.ProductName);
    }

    [Fact]
    public void Removes_From_Cart()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateShoppingCartController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        int itemId = -1;

        // Act
        var result = controller.RemoveFromCart(itemId);

        // Assert - Database
        var item = dbContext.ShoppingCartItems.FirstOrDefault(i => i.Id == itemId);
        item.ShouldBeNull();
    }

    private static ShoppingCartController CreateShoppingCartController(IServiceScope scope)
    {
        return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
        {
            ControllerContext = BuildContext("-21")
        };
    }
}
