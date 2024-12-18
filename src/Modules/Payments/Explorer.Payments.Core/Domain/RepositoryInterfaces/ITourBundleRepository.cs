using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ITourBundleRepository
{
    PagedResult<TourBundle> GetAll();
    TourBundle Get(long id);
    TourBundle Create(TourBundle tourBundle);
    TourBundle Delete(TourBundle tourBundle);
    void Save(TourBundle tourBundle);
    TourBundle GetById(long tourBundleId);
    PagedResult<TourBundle> GetAllPublished();   
}
