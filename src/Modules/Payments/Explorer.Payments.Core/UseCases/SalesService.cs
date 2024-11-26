using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Sales;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class SalesService : BaseService<SalesDto, Sales>, ISalesService
{
	private readonly ISalesRepository _salesRepository;
	private readonly IInternalSalesService _internalSalesService;

	public SalesService(IMapper mapper, ISalesRepository salesRepository, IInternalSalesService internalSalesService) : base(mapper)
	{
		_salesRepository = salesRepository;
		_internalSalesService = internalSalesService;
	}

	public async Task<Result> Create(SalesInputDto inputSales)
	{
		var sales = new SalesDto
		{
			EndsAt = inputSales.EndsAt,
			Discount = inputSales.Discount,
			TourIds = inputSales.TourIds
		};

		if (!_internalSalesService.AreAuthorTours(inputSales.AuthorId, inputSales.TourIds))
			return Result.Fail(FailureCode.NotFound).WithError("Author ID mismatch");

		try
		{
			await _salesRepository.Create(MapToDomain(sales));
			return Result.Ok();
		}
		catch (Exception ex)
		{
			return Result.Fail($"{ex.Message}");
		}
	}
}
