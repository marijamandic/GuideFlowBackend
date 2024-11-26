using Explorer.Payments.API.Dtos.Sales;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ISalesService
{
	Task<Result> Create(SalesInputDto sales);
}
