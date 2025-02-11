using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity, IDtoConvertable
    {
        [Column("Order_Date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column("Buyer_Email")]
        public required string BuyerEmail { get; set; }

        public ShippingAddress ShippingAddress { get; set; } = null!;

        public DeliveryMethod DeliveryMethod { get; set; } = null!;

        public PaymentSummary PaymentSummary { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; } = [];
        
        [Column("Sub_Total")]
        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column("Payment_Intent_Id")]
        public required string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal - Discount + DeliveryMethod.Price;
        }
    }
}