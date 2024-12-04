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

	public async Task<Sales> GetById(long id)
	{
		try
		{
			var result = await _sales.FirstOrDefaultAsync(s => s.Id == id);
			if (result is null) throw new ArgumentException(nameof(result));
			return result;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task Update(Sales sales)
	{
		try
		{
			_paymentsContext.Update(sales);
			await _paymentsContext.SaveChangesAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task Delete(Sales sales)
	{
		try
		{
			_paymentsContext.Remove(sales);
			await _paymentsContext.SaveChangesAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}
    public async Task<IEnumerable<Sales>> GetAll()
    {
        try
        {
            return await _sales.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Greška prilikom dohvatanja podataka: {ex.Message}");
        }
    }


}
