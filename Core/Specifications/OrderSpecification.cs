using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string email) : base (e => e.Buyeremail == email)
        {
            AddInclude(oi => oi.OrderItems);
            AddInclude(dm => dm.DeliveryMethod);
            AddOrderByDescending(od => od.OrderDate);
        }

        public OrderSpecification(string email, int id) : base(e => e.Buyeremail == email & e.Id == id)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }

        public OrderSpecification(string paymentIntentId, bool isPaymentIntent): base(x => x.PaymentIntentId == paymentIntentId)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }
    }
}