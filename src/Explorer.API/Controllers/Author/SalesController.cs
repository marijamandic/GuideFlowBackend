using Explorer.Payments.API.Dtos.Sales;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/sales")]
public class SalesController : BaseApiController
{
	private readonly ISalesService _salesService;

	public SalesController(ISalesService salesService)
	{
		_salesService = salesService;
	}

	[HttpPost]
	public async Task<ActionResult> Create([FromBody] SalesInputDto sales)
	{
		sales.AuthorId = int.Parse(User.FindFirst("id")!.Value);
		var result = await _salesService.Create(sales);
		return CreateResponse(result);
	}
}
