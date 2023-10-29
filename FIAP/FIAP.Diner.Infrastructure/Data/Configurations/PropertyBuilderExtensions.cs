using FIAP.Diner.Domain.Common;
using FIAP.Diner.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Diner.Infrastructure.Data.Configurations;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<ProductId> IsId(this PropertyBuilder<Id> propertyBuilder) =>
        propertyBuilder.HasConversion(
            id => id.Value,
            value => new Id(new Guid(value.ToString())));

    public static PropertyBuilder<CPF> IsCpf(this PropertyBuilder<CPF> propertyBuilder) =>
        propertyBuilder.HasConversion(
            cpf => cpf.Number,
            number => new CPF(number));

    public static PropertyBuilder<T> IsEnum<T>(this PropertyBuilder<T> propertyBuilder) =>
        propertyBuilder.HasConversion(
            enumeration => enumeration!.ToString(),
            value => (T)Enum.Parse(typeof(T), value));

    public static PropertyBuilder<Price> IsMoney(this PropertyBuilder<Price> propertyBuilder) =>
        propertyBuilder.HasConversion(
            price => price.Amount,
            value => new Price(value)).HasColumnType("money");
}