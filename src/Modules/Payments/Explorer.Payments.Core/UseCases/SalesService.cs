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
			EndsAt = inputSales.EndsAt.ToUniversalTime(),
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

	public async Task<Result> Update(SalesDto sales, int authorId)
	{
		if (!_internalSalesService.AreAuthorTours(authorId, sales.TourIds))
			return Result.Fail(FailureCode.NotFound).WithError("Author ID mismatch");

		try
		{
			var oldSales = await _salesRepository.GetById(sales.Id);
			oldSales.Update(MapToDomain(sales));
			await _salesRepository.Update(oldSales);
			return Result.Ok();
		}
		catch (Exception ex)
		{
			return Result.Fail(FailureCode.InvalidArgument).WithError($"{ex.Message}");
		}
	}

	public async Task<Result> Delete(int id, int authorId)
	{
		try
		{
			var sales = await _salesRepository.GetById(id);
			if (!_internalSalesService.AreAuthorTours(authorId, sales.TourIds.Select(id => (int)id).ToList()))
				return Result.Fail(FailureCode.NotFound).WithError("Author ID mismatch");

			await _salesRepository.Delete(sales);
			return Result.Ok();
		}
		catch (Exception ex)
		{
			return Result.Fail(FailureCode.NotFound).WithError($"{ex.Message}");
		}
	}

    public async Task<IEnumerable<SalesDto>> GetAll()
    {
        try
        {
            var sales = await _salesRepository.GetAll();

            if (sales == null || !sales.Any())
            {
                return Enumerable.Empty<SalesDto>();
            }

            var salesDtos = sales
                .Select(MapToDto)
                .OrderByDescending(dto => dto.Discount)
                .ToList();

            return salesDtos;
        }
        catch (Exception ex)
        {
            throw new Exception($"Greška prilikom dohvatanja podataka: {ex.Message}");
        }
    }




}
