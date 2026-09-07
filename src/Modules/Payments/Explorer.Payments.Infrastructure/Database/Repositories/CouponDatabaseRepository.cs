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
    public class CouponDatabaseRepository : ICouponRepository
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly DbSet<Coupon> _coupons;

        public CouponDatabaseRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
            _coupons = _paymentsContext.Set<Coupon>();
        }

        public Coupon Create(Coupon entity)
        {
            _coupons.Add(entity);
            _paymentsContext.SaveChanges();
            return entity;
        }

        public Coupon? GetById(long id)
        {
            return _coupons.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public Coupon? GetByCode(string code)
        {
            return _coupons.AsNoTracking().FirstOrDefault(c => c.Code == code);
        }

        public List<Coupon> GetByAuthorId(long authorId)
        {
            return _coupons.AsNoTracking().Where(c => c.AuthorId == authorId).ToList();
        }

        public Coupon Update(Coupon coupon)
        {
            _coupons.Update(coupon);
            _paymentsContext.SaveChanges();
            return coupon;
        }

        public void Delete(long id)
        {
            var coupon = _coupons.FirstOrDefault(c => c.Id == id);
            if (coupon != null)
            {
                _coupons.Remove(coupon);
                _paymentsContext.SaveChanges();
            }
        }

        public void Save(Coupon coupon)
        {
            _paymentsContext.Entry(coupon).State = EntityState.Modified;
            _paymentsContext.SaveChanges();
        }
    }
}
