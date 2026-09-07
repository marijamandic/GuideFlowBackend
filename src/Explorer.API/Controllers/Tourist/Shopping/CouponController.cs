using Explorer.Payments.API.Dtos.Coupons;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping/coupons")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        

        [HttpPut("redeem/{code}")]
        public ActionResult Redeem([FromRoute] string code)
        {
            var result = _couponService.RedeemCoupon(code);
            return CreateResponse(result);
        }

      
    }
}
