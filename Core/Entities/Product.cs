using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        [Column("Product_Name")]
        public required string Name { get; set; }

        [Column("Product_Description")]
        public required string Description { get; set; }

        [Column("Product_Type")]
        public required string Type { get; set; }

        [Column("Product_Brand")]
        public required string Brand { get; set; }

        [Column("Picture_Url")]
        public required string PictureUrl { get; set; }

        [Column("Product_Price")]
        public decimal Price { get; set; }

        [Column("Quantity_In_Stock")]
        public int QuantityInStock { get; set; }
    }
}