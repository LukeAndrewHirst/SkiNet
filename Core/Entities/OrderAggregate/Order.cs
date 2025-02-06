using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public required string Buyeremail { get; set; }

        public ShippingAddress ShippingAddress { get; set; } = null!;

        public DeliveryMethod DeliveryMethod { get; set; } = null!;

        public PaymentSummary PaymentSummary { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; } = [];
        
        [Column("Product_Id")]
        public decimal SubTotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column("Payment_Intent_Id")]
        public required string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}