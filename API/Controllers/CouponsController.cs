using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CouponsController(ICouponService couponService) : BaseApiController
    {
        [HttpGet("{code}")]
        public async Task<ActionResult<AppCoupon>> ValidateCode(string code)
        {
            var coupon = await couponService.GetCouponFromPromotionCode(code);
            if(coupon == null) return BadRequest("Invalid voucher code");

            return coupon;
        }
    }
}