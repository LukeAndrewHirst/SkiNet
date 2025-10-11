using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class CouponService : ICouponService
    {
        public CouponService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
        }

        public async Task<AppCoupon?> GetCouponFromPromotionCode(string code)
        {
            var promotionService = new PromotionCodeService();
            var couponService = new Stripe.CouponService();

            var options = new PromotionCodeListOptions
            {
                Code = code
            };

            var promotionCodes = await promotionService.ListAsync(options);
            var promotionCode = promotionCodes.FirstOrDefault();

            if (promotionCode is null) return null;
            if (string.IsNullOrEmpty(promotionCode.Code)) return null;

            var coupon = await couponService.GetAsync(promotionCode.Code);
            if (coupon == null) return null;

            return new AppCoupon
            {
                Name = coupon.Name,
                AmountOff = coupon.AmountOff,
                PercentOff = coupon.PercentOff,
                CouponId = coupon.Id,
                PromotionCode = promotionCode.Code
            };   
        }
    }
}