using FIAP.Diner.Domain.Orders;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Diner.Infrastructure.Data.Modules.Orders;

public class OrderTrackingMapping : IEntityTypeConfiguration<OrderTracking>
{
    public void Configure(EntityTypeBuilder<OrderTracking> builder)
    {
        builder.ToTable("OrderTrackings");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(c => c.OrderStatus)
            .IsEnum()
            .HasColumnName("Status")
            .IsRequired();

        builder.Property(c => c.When)
            .IsRequired();
    }
}