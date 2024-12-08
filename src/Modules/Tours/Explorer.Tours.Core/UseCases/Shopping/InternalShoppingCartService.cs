using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Author;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Shopping;

public class InternalShoppingCartService : IInternalShoppingCartService
{
	private readonly ITourService _tourService;

	public InternalShoppingCartService(ITourService tourService)
	{
		_tourService = tourService;
	}

	public TourDto GetById(long id)
	{
		try
		{
			var result = _tourService.Get((int)id);
			return result.Value;
		}
		catch (Exception)
		{
			throw;
		}
	}
}
