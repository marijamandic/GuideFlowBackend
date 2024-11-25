using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class TourBundleRepository: ITourBundleRepository
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly DbSet<TourBundle> _tourBundles;

        public TourBundleRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext =  paymentsContext;
            _tourBundles = _paymentsContext.Set<TourBundle>();

        }

        public TourBundle Create(TourBundle bundle) 
        {
            _tourBundles.Add(bundle);
            _paymentsContext.SaveChanges();
            return bundle;
        }

        public void Save(TourBundle bundle) 
        {
            _paymentsContext.Entry(bundle).State = EntityState.Modified;
            _paymentsContext.SaveChanges();
        }

    }
}
