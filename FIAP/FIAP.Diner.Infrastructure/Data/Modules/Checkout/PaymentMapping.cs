using FIAP.Diner.Domain.Checkout;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Diner.Infrastructure.Data.Modules.Checkout;

public class PaymentMapping : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(c => c.ShoppingCart)
            .IsId()
            .HasColumnName("ShoppingCartId")
            .IsRequired();

        builder.Property(p => p.PayedAt)
            .IsRequired(false);

        builder.Property(p => p.RefusedAt)
            .IsRequired(false);

        builder.Property(p => p.Status)
            .IsEnum()
            .IsRequired();

        builder.Property(p => p.Method)
            .IsEnum()
            .IsRequired();

        builder.Property(c => c.Amount)
            .IsMoney()
            .HasColumnName("Amount")
            .IsRequired();
    }
}