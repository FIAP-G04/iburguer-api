using FIAP.Diner.Domain.Orders;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Diner.Infrastructure.Data.Modules.Orders;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(c => c.ShoppingCart)
            .IsId()
            .HasColumnName("ShoppingCartId")
            .IsRequired();

        builder.Property(o => o.Number)
            .HasConversion(
                orderNumber => orderNumber.Value,
                number => new OrderNumber(number))
            .HasColumnName("OrderNumber")
            .IsRequired();

        builder.Property(o => o.WithdrawalCode)
            .HasConversion(
                withdrawalCode => withdrawalCode.Code,
                code => new WithdrawalCode(code))
            .HasColumnName("WithdrawalCode")
            .IsRequired();

        builder.HasMany(p => p.Trackings)
            .WithOne()
            .HasForeignKey(i => i.Order)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}