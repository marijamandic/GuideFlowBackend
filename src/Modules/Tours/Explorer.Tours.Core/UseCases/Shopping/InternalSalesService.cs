using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Author;

namespace Explorer.Tours.Core.UseCases.Shopping;

public class InternalSalesService : IInternalSalesService
{
	private readonly ITourService _tourService;

	public InternalSalesService(ITourService tourService)
	{
		_tourService = tourService;
	}

	public bool AreAuthorTours(int authorId, List<int> tourIds)
	{
		try
		{
			var result = _tourService.GetTourIdsByAuthorId(authorId);
			return tourIds.Select(id => (long)id).All(result.Value.Contains);
		}
		catch (Exception)
		{
			return false;
		}
	}
}
