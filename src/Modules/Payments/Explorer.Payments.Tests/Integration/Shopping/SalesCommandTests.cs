using Explorer.API.Controllers.Author;
using Explorer.Payments.API.Dtos.Sales;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration.Shopping;

public class SalesCommandTests : BasePaymentsIntegrationTests
{
	public SalesCommandTests(PaymentsTestFactory factory) : base(factory) { }

	[Fact]
	public async Task Creates()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateSalesController(scope);
		var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
		var salesInput = new SalesInputDto
		{
			EndsAt = DateTime.Now.AddDays(7),
			Discount = 99,
			TourIds = new List<int> { -1 }
		};

		// Act
		var result = await controller.Create(salesInput);

		// Assert - Database
		var sales = dbContext.Sales.FirstOrDefault(s => s.Discount == salesInput.Discount);
		sales.ShouldNotBeNull();
	}

	private static SalesController CreateSalesController(IServiceScope scope)
	{
		return new SalesController(scope.ServiceProvider.GetRequiredService<ISalesService>())
		{
			ControllerContext = BuildContext("101")
		};
	}
}
