using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/tours")]
public class TourController : BaseApiController
{
	private readonly ITourService _tourService;

	public TourController(ITourService tourService)
	{
		_tourService = tourService;
	}

	// dobavlja detalje tura(opis, level, tagove) koje se nalaze u shopping cartu turiste
	[HttpGet("details")]
	public async Task<ActionResult<PagedResult<TourDetailsDto>>> GetDetailsByTouristId()
	{
		int touristId = int.Parse(User.FindFirst("id")!.Value);
		var result = await _tourService.GetDetailsByTouristId(touristId);
		return CreateResponse(result);
	}
}
