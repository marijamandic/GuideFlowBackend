using Explorer.Payments.API.Dtos.Coupons;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<CouponDto> CreateCoupon(CreateCouponDto dto);
        Result RedeemCoupon(string code);
        Result DeleteCoupon(long couponId);
        Result<CouponDto> GetByCode(string code);
        Result<List<CouponDto>> GetByAuthorId(long authorId);
        Result<CouponDto> UpdateCoupon(long couponId, CouponDto dto);
    }
}
