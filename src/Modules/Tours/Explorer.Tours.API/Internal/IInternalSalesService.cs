namespace Explorer.Tours.API.Internal;

public interface IInternalSalesService
{
	bool AreAuthorTours(int authorId, List<int> tourIds);
}
