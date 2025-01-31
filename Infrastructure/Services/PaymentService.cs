using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class PaymentService(IConfiguration configuration, ICartService cartService, IGenericRepository<Core.Entities.Product> productRepository, 
    IGenericRepository<DeliveryMethod> deliveryMethodRepository) : IPaymentService
    {
        public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecterKey"];

            var cart = await cartService.GetCartAsync(cartId);
            if(cart == null) return null;

            var shippingPrice = 0m;

            if(cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await deliveryMethodRepository.GetByIdAsync((int)cart.DeliveryMethodId);
                if(deliveryMethod == null) return null;

                shippingPrice = deliveryMethod.Price;
            }

            foreach(var item in cart.Items)
            {
                var productItem = await productRepository.GetByIdAsync(item.ProductId);
                if(productItem == null) return null;

                if(item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent? intent = null;

            if(string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.Items.Sum(p => p.Quantity * (p.Price * 100)) + (long)shippingPrice, Currency = "usd", PaymentMethodTypes = ["card"] 
                };

                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(p => p.Quantity * (p.Price * 100)) + (long)shippingPrice * 1000,
                };
                intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            }

            await cartService.SetCartAsync(cart);

            return cart;
        }
    }
}