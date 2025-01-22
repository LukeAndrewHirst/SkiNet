using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CartItem
    {
        [Column("Product_Id")]
        public int ProductId { get; set; }

        [Column("Product_Name")]
        public required string ProductName { get; set; }

        [Column("Picture_Url")]
        public required string PictureUrl { get; set; }

        [Column("Brand")]
        public required string Brand { get; set; }

        [Column("Type")]
        public required string Type { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }
    }
}