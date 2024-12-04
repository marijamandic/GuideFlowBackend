using Explorer.Payments.Core.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ICouponRepository
    {
        Coupon Create(Coupon coupon);
        Coupon? GetByCode(string code);
        List<Coupon> GetByAuthorId(long authorId);
        void Save(Coupon coupon);
        void Delete(long couponId);
        public Coupon? GetById(long id);
        Coupon Update(Coupon coupon);

    }
}