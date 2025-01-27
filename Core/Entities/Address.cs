using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Address : BaseEntity
    {
        [Column("Line_1")]
        public required string Line1 { get; set; }

        [Column("Line_2")]
        public string? Line12 { get; set; }

        public required string Country { get; set; }

        public required string City { get; set; }

        public required string State { get; set; }

        [Column("Postal_Code")]
        public required string PostalCode { get; set; }
    }
}