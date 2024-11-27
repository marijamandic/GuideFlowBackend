using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ITourBundleRepository
    {
        PagedResult<TourBundle> GetAll();
        TourBundle Get(long id);
        TourBundle Create(TourBundle tourBundle);

        TourBundle Delete(TourBundle tourBundle);

        void Save(TourBundle tourBundle);

        TourBundle GetById(long tourBundleId);
        
    }
}
