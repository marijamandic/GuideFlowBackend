using Explorer.Payments.API.Dtos.Sales;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ISalesService
{
	Task<Result> Create(SalesInputDto sales);
	Task<Result> Update(SalesDto sales, int authorId);
	Task<Result> Delete(int id, int authorId);
    Task<IEnumerable<SalesDto>> GetAll();
}
