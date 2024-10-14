using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ITourSpecificationRepository
    {
        bool Exists(int tourSpecificationId);
        TourSpecifications? GetById(int tourSpecificationId);
        IEnumerable<TourSpecifications> GetAll();
        TourSpecifications Create(TourSpecifications tourSpecification);
        void Update(TourSpecifications tourSpecification);
        void Delete(long tourSpecificationId);
    }
}
