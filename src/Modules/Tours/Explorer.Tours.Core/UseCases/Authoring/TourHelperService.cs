using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Authoring;

public class TourHelperService : BaseService<TourDto, Tour>, IInternalTourHelperService
{
	private readonly ITourRepository _repository;

	public TourHelperService(IMapper mapper, ITourRepository repository) : base(mapper)
	{
		_repository = repository;
	}

	public Result<TourDto> Get(int id)
	{
		try
		{
			return MapToDto(_repository.Get(id));
		}
		catch (KeyNotFoundException e)
		{
			return Result.Fail(FailureCode.NotFound).WithError(e.Message);
		}
	}
}
