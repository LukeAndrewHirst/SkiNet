using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.ShortName).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(dm => dm.DeliveryTime).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(dm => dm.Description).HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(dm => dm.Price).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}