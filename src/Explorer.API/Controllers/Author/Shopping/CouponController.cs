using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Coupons;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Coupons
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/shopping/coupons")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        public ActionResult<CouponDto> Create([FromBody] CreateCouponDto createCouponDto)
        {
            var result = _couponService.CreateCoupon(createCouponDto);
            return CreateResponse(result);
        }

        //[HttpPut("redeem/{code}")]
        //public ActionResult Redeem([FromRoute] string code)
        //{
        //    var result = _couponService.RedeemCoupon(code);
        //    return CreateResponse(result);
        //}

        [HttpDelete("{couponId:long}")]
        public ActionResult Delete([FromRoute] long couponId)
        {
            var result = _couponService.DeleteCoupon(couponId);
            return CreateResponse(result);
        }

        [HttpGet("code/{code}")]
        public ActionResult<CouponDto> GetByCode([FromRoute] string code)
        {
            var result = _couponService.GetByCode(code);
            return CreateResponse(result);
        }

        [HttpGet("author/{authorId:long}")]
        public ActionResult<List<CouponDto>> GetByAuthorId([FromRoute] long authorId)
        {
            var result = _couponService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpPut("{couponId:long}")]
        public ActionResult<CouponDto> Update([FromRoute] long couponId, [FromBody] CouponDto dto)
        {
            var result = _couponService.UpdateCoupon(couponId, dto);
            return CreateResponse(result);
        }
    }
}
