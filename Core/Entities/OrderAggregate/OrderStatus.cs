namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        Pending,
        PaymentReceved,
        PaymentFailed,
        PaymentMismatch,
        Refunded
    }
}