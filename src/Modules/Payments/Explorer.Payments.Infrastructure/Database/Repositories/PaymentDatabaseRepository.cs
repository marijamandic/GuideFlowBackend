using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Payments;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class PaymentDatabaseRepository:IPaymentRepository
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly DbSet<Payment> _payments;

        public PaymentDatabaseRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
            _payments = _paymentsContext.Set<Payment>();
        }

        public Payment Create(Payment entity)
        {
            _payments.Add(entity);
            _paymentsContext.SaveChanges();
            return entity;
        }

        public PagedResult<Payment> GetAllByTouristId(long touristId)
        {
            List<Payment> payments = _payments.Where(p => p.TouristId == touristId).Include(p => p.PaymentItems).ToList();
            if (payments.Count == 0) throw new KeyNotFoundException("Not found any payment for touristId: " + touristId);
            return new PagedResult<Payment>(payments, payments.Count);

        }

        public void Save(Payment payment)
        {
            _paymentsContext.Entry(payment).State = EntityState.Modified;
            _paymentsContext.SaveChanges();
        }
    }
}
