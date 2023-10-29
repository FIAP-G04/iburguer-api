using FIAP.Diner.Domain.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIAP.Diner.Infrastructure.Data.Modules.Menu;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Id)
            .HasConversion(new ValueConverter<ProductId, Guid>(
                v => v.Value,
                v => new ProductId(v)
            ))
            .IsRequired();

        builder.HasKey(c => c.Id);

        builder.Property(p => p.Name)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.Category)
               .HasConversion(
            c => c.ToString(),
            c => (Category)Enum.Parse(typeof(Category), c))
               .IsRequired();

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(c => c.Amount)
                 .HasColumnName("Price")
                 .IsRequired();
        });

        builder.OwnsOne(p => p.PreparationTime, time =>
        {
            time.Property(c => c.Minutes)
                .HasColumnName("PreparationTime")
                .IsRequired();
        });

        builder.Property(p => p.Enabled)
            .IsRequired();

    }
}