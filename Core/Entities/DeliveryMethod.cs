using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class DeliveryMethod : BaseEntity
    {
        [Column("Short_Name")]
        public required string ShortName { get; set; }

        [Column("Delivery_Time")]
        public required string DeliveryTime { get; set; }
        
        public required string Description { get; set; }

        public decimal Price { get; set; }
    }
}