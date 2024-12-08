using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal;

public interface IInternalShoppingCartService
{
	TourDto GetById(long id);
}
