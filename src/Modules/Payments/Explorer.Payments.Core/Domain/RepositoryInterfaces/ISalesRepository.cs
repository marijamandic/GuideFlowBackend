namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ISalesRepository
{
	Task Create(Sales sales);
}
