using Core.Entities.OrderAggregate;

namespace API.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public required string Buyeremail { get; set; }

        public required ShippingAddress ShippingAddress { get; set; }

        public required string DeliveryMethod { get; set; }

        public decimal DeliveryFee { get; set; }

        public required PaymentSummary PaymentSummary { get; set; }

        public required List<OrderItemDto> OrderItems { get; set; }
        
        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public required string Status { get; set; }

        public decimal Total { get; set; }

        public required string PaymentIntentId { get; set; }
    }
}