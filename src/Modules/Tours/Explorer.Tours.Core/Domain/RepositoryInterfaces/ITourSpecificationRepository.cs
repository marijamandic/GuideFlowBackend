using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourSpecificationRepository
    {
        TourSpecification Create(TourSpecification tourSpecification);
        TourSpecification Update(TourSpecification tourSpecification);
        void Delete(long tourSpecificationId);
        TourSpecification Get(long tourSpecificationId);
        PagedResult<TourSpecification> GetPaged(int page, int pageSize);
        TourSpecification GetByUserId(long userId);
        void AddTransportRatings(long id, IEnumerable<TransportRating> transportRatings);

    }
}
