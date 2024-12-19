using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class TourBundleRepository: ITourBundleRepository
{
    private readonly PaymentsContext _paymentsContext;
    private readonly DbSet<TourBundle> _tourBundles;

    public TourBundleRepository(PaymentsContext paymentsContext)
    {
        _paymentsContext =  paymentsContext;
        _tourBundles = _paymentsContext.Set<TourBundle>();
    }

    public PagedResult<TourBundle> GetAll()
    {
        var tourBundles = _tourBundles.ToList();
        return new PagedResult<TourBundle>(tourBundles, tourBundles.Count);
    }

    public TourBundle Get(long id)
    {
        var entity = _tourBundles.Where(tb => tb.Id == id).FirstOrDefault();
        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        return entity;
    }

    public TourBundle GetById(long tourBundleId)
    {
        var tourBundle = _tourBundles.Find(tourBundleId);
        if (tourBundle == null) throw new NullReferenceException("Tour Bundle Not Found");
        return tourBundle;
    }
    public TourBundle Create(TourBundle tourBundle) 
    {
        _tourBundles.Add(tourBundle);
        _paymentsContext.SaveChanges();
        return tourBundle;
    }

    public TourBundle Delete(TourBundle tourBundle)
    {
        _tourBundles.Remove(tourBundle);
        _paymentsContext.SaveChanges();
        return tourBundle;
    }

    public void Save(TourBundle bundle) 
    {
        _paymentsContext.Entry(bundle).State = EntityState.Modified;
        _paymentsContext.SaveChanges();
    }

	public PagedResult<TourBundle> GetAllPublished()
	{
	    var bundles = _tourBundles.Where(b => b.Status == BundleStatus.Published).ToList();
        return new PagedResult<TourBundle>(bundles, bundles.Count);
	}
}
