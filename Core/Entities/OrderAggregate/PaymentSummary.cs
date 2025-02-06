using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.OrderAggregate
{
    public class PaymentSummary
    {
        public int Last4 { get; set; }

        public required string Brand { get; set; }

        
        [Column("Expiry_Month")]
        public int ExpMonth { get; set; }

        [Column("Expiry_Year")]
        public int ExpYear { get; set; }
    }
}