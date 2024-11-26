namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ISalesRepository
{
	Task Create(Sales sales);
	Task<Sales> GetById(long id);
	Task Update(Sales sales);
	Task Delete(long id);
}
