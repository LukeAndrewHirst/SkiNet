using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class PaymentService(IConfiguration configuration, ICartService cartService, IUnitOfWork unitOfWork) : IPaymentService
    {
        public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var cart = await cartService.GetCartAsync(cartId) ?? throw new Exception("Cart not found");
            var shippingPrice = await GetShippingPriceAsync(cart) ?? 0;
            
            await ValidateCartItemsInCartAsync(cart);
            
            var subtotal = CalculateSubtotal(cart);
            if (cart.Coupon != null)
            {
                subtotal = await ApplyDiscountAsync(cart.Coupon, subtotal);
            }
            
            var total = subtotal + shippingPrice;

            await CreateUpdatePaymentIntentAsync(cart, total);
            await cartService.SetCartAsync(cart);
            
            return cart;
        }

        public async Task<string> RefundPayment(string paymentIntentId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId
            };

            var refundService = new RefundService();
            var result = await refundService.CreateAsync(refundOptions);

            return result.Status;
        }

        private async Task CreateUpdatePaymentIntentAsync(ShoppingCart cart, long total)
        {
            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = total,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };

                var intent = await service.CreateAsync(options);
                
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = total
                };
                await service.UpdateAsync(cart.PaymentIntentId, options);
            }
        }

        private async Task<long> ApplyDiscountAsync(AppCoupon appCoupon, long amount)
        {
            var couponService = new Stripe.CouponService();
            var coupon = await couponService.GetAsync(appCoupon.CouponId);

            if(coupon.AmountOff.HasValue)
            {
                amount -= (long)coupon.AmountOff * 100;
            }

            if(coupon.PercentOff.HasValue)
            {
                var discount = amount * (coupon.PercentOff.Value / 100);
                amount -= (long)discount;
            }
            return amount;
        }

        private long CalculateSubtotal(ShoppingCart cart)
        {
            var itemTotal = cart.Items.Sum(x => x.Quantity * x.Price * 100);
            return (long)itemTotal;
        }

        private async Task ValidateCartItemsInCartAsync(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
            var productItem = await unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(item.ProductId) ?? throw new Exception("Unable to locate cart");
            
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
        }

        private async Task<long?> GetShippingPriceAsync(ShoppingCart cart)
        {
            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)cart.DeliveryMethodId)?? throw new Exception("Delivery method not found");
                return (long)deliveryMethod.Price * 100;
            }
            return null;
        }
    }
}