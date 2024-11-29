using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IPaymentRepository
    {
        Payment Create(Payment entity);
        PagedResult<Payment> GetAllByTouristId(long touristId);
        void Save(Payment payment);
    }
}
