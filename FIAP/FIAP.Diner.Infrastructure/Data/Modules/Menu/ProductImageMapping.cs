using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIAP.Diner.Infrastructure.Data.Modules.Menu;

public class ProductImageMapping : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("Images");

        builder.Property(c => c.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.OwnsOne(p => p.Url, url =>
        {
            url.Property(c => c.Value)
                .HasColumnName("Url")
                .IsRequired();
        });

    }
}