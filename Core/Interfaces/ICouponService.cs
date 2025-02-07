using Core.Entities;

namespace Core.Interfaces
{
    public interface ICouponService
    {
        Task<AppCoupon?> GetCouponFromPromotionCode(string code);
    }
}