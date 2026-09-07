using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Coupons;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.Payments;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class CouponService : BaseService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository, IMapper mapper)
            : base(mapper)
        {
            _couponRepository = couponRepository;
        }

        public Result<CouponDto> CreateCoupon(CreateCouponDto dto)
        {
            try
            {
               
                var code = GenerateRandomCode();

                var coupon = new Coupon(
                    AuthorId: dto.AuthorId,
                    TourId: dto.TourId,
                    Code: code,
                    Discount: dto.Discount,
                    ExpiryDate: dto.ExpiryDate,
                    ValidForAllTours: dto.ValidForAllTours
                );

                _couponRepository.Create(coupon);

                return Result.Ok(MapToDto(coupon));
            }
            catch (Exception ex)
            {
                return Result.Fail<CouponDto>(ex.Message);
            }
        }

        public Result RedeemCoupon(string code)
        {
            try
            {

                var coupon = _couponRepository.GetByCode(code);
                if (coupon == null)
                    return Result.Fail("Invalid coupon code.");

                coupon.Redeem();
                _couponRepository.Save(coupon);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result DeleteCoupon(long couponId)
        {
            try
            {
                _couponRepository.Delete(couponId);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<CouponDto> GetByCode(string code)
        {
            try
            {
                var coupon = _couponRepository.GetByCode(code);
                if (coupon == null)
                    return Result.Fail<CouponDto>("Coupon not found");

                return Result.Ok(MapToDto(coupon));
            }
            catch (Exception ex)
            {
                return Result.Fail<CouponDto>(ex.Message);
            }
        }

        public Result<List<CouponDto>> GetByAuthorId(long authorId)
        {
            try
            {
                var coupons = _couponRepository.GetByAuthorId(authorId);
                return Result.Ok(coupons.Select(MapToDto).ToList());
            }
            catch (Exception ex)
            {
                return Result.Fail<List<CouponDto>>(ex.Message);
            }
        }
        public Result<CouponDto> UpdateCoupon(long couponId, CouponDto dto)
        {
            try
            {
                var coupon = _couponRepository.GetById(couponId);
                if (coupon == null)
                    return Result.Fail<CouponDto>("Coupon not found");


                coupon.UpdateDetails(
                    tourId: dto.TourId,
                    code: dto.Code,
                    discount: dto.Discount,
                    expiryDate: dto.ExpiryDate,
                    validForAllTours: dto.ValidForAllTours
                );

                _couponRepository.Save(coupon);

                return Result.Ok(MapToDto(coupon));
            }
            catch (Exception ex)
            {
                return Result.Fail<CouponDto>(ex.Message);
            }
        }


        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
