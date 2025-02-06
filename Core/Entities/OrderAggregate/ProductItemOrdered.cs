using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        [Column("Product_Id")]
        public int ProductId { get; set; }

        [Column("Product_Name")]
        public required string ProductName { get; set; }

        [Column("Picture_Url")]
        public required string PictureUrl { get; set; }
    }
}