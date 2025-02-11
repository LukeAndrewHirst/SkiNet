using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string email) : base (e => e.BuyerEmail == email)
        {
            AddInclude(oi => oi.OrderItems);
            AddInclude(dm => dm.DeliveryMethod);
            AddOrderByDescending(od => od.OrderDate);
        }

        public OrderSpecification(string email, int id) : base(e => e.BuyerEmail == email & e.Id == id)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }

        public OrderSpecification(string paymentIntentId, bool isPaymentIntent): base(x => x.PaymentIntentId == paymentIntentId)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }

        public OrderSpecification(OrderSpecParams specParams) : base(x => string.IsNullOrEmpty(specParams.Status) || x.Status == ParseStatus(specParams.Status))
        {
             AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
            ApplyPaging(specParams.PageSize * (specParams.PageIndex -1), specParams.PageSize);
            AddOrderByDescending(x => x.OrderDate);
        }

        public OrderSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }

        private static OrderStatus? ParseStatus(string status)
        {
            if(Enum.TryParse<OrderStatus>(status, true, out var result)) return result;

            return null;
        }
    }
}