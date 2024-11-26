using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class SalesDatabaseRepository : ISalesRepository
{
	private readonly PaymentsContext _paymentsContext;
	private readonly DbSet<Sales> _sales;

	public SalesDatabaseRepository(PaymentsContext paymentsContext)
	{
		_paymentsContext = paymentsContext;
		_sales = _paymentsContext.Set<Sales>();
	}


	public async Task Create(Sales sales)
	{
		_sales.Add(sales);

		try
		{
			await _paymentsContext.SaveChangesAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}
}
