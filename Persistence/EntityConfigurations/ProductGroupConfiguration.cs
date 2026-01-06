using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasData([
        new ProductGroup
        {
            Id = Guid.Parse("94844dc3-e661-4ebd-874a-e82947113313"),
            Name = "Майки"
        },
        new ProductGroup
        {
            Id = Guid.Parse("60e40827-884d-4d17-abab-e02cd25734c4"),
            Name = "Рюкзаки"
        },
        new ProductGroup
        {
            Id = Guid.Parse("ed21be16-b60c-4b26-a684-a5c4570810fe"),
            Name = "Блокноты"
        },
        new ProductGroup
        {
            Id = Guid.Parse("0942222e-e07a-48ad-b975-7d84ee0347fe"),
            Name = "Шоперы"
        },
        new ProductGroup
        {
            Id = Guid.Parse("c896a78c-3d16-4499-a86f-f4efa1a2e429"),
            Name = "Чашки"
        },
        new ProductGroup
        {
            Id = Guid.Parse("e83ebc58-0e18-4e9e-b240-78a7be67537f"),
            Name = "Термосы"
        },
        new ProductGroup
        {
            Id = Guid.Parse("b5c17855-73b5-4322-84e6-b5b29985575e"),
            Name = "Жилеты"
        },
        new ProductGroup
        {
            Id = Guid.Parse("14089db0-44de-4317-99a7-3dd01b4a154a"),
            Name = "Байки"
        },
        new ProductGroup
        {
            Id = Guid.Parse("60980b50-d197-4c9d-a2bf-3d96715386d0"),
            Name = "Бутылки"
        },
        new ProductGroup
        {
            Id = Guid.Parse("e3510025-fe7d-4b14-977e-95697d863a57"),
            Name = "Кепки"
        },
        new ProductGroup
        {
            Id = Guid.Parse("ad89c90b-f011-496e-8f6f-2fd626ecb9e2"),
            Name = "Стикеры"
        },
        new ProductGroup
        {
            Id = Guid.Parse("36f0120b-b0f4-4cde-9816-cae55b0602ff"),
            Name = "Маскоты"
        },
        new ProductGroup
        {
            Id = Guid.Parse("a963d22e-ca6f-4f33-8e7b-f571f780e057"),
            Name = "Курсы"
        },]);
    }
}
