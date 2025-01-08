using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar").HasMaxLength(3000).IsRequired();
            builder.Property(p => p.Type).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(p => p.Brand).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(p => p.PictureUrl).HasColumnType("varchar").HasMaxLength(3000).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.QuantityInStock).HasColumnType("int").IsRequired();
        }
    }
}