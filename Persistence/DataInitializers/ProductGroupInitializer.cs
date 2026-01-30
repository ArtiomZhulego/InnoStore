using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DataInitializers.Abstractions;

namespace Persistence.DataInitializers;

public class ProductGroupInitializer(InnoStoreContext context) : IDataInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (await context.ProductGroups.AnyAsync(cancellationToken))
        {
            return;
        }

        var productNames = new (string Ru, string En)[]
        {
            ("Майка (текущий год)", "T-Shirt (current year)"),
            ("Рюкзак", "Backpack"),
            ("Шопер", "Tote Bag"),
            ("Блокнот большой тв", "Large Hardcover Notebook"),
            ("Термочашка", "Travel Mug"),
            ("Термос", "Thermos"),
            ("Жилет", "Vest"),
            ("Байка", "Hoodie"),
            ("Бутылка для воды", "Water Bottle"),
            ("Кепка", "Cap"),
            ("Стикер пак", "Sticker Pack"),
            ("Майка (прошлый год)", "T-Shirt (last year)"),
            ("Маскот отдела", "Department Mascot"),
            ("Оплата курса", "Course Payment")
        };

        var productGroups = productNames.Select(p =>
        {
            var id = Guid.NewGuid();
            return new ProductGroup
            {
                Id = id,
                Localizations =
                [
                    new ProductGroupLocalization
                    {
                        Id = Guid.NewGuid(),
                        ProductGroupId = id,
                        LanguageISOCode = "ru",
                        Name = p.Ru
                    },
                    new ProductGroupLocalization
                    {
                        Id = Guid.NewGuid(),
                        ProductGroupId = id,
                        LanguageISOCode = "en",
                        Name = p.En
                    }
                ]
            };
        }).ToArray();

        await context.ProductGroups.AddRangeAsync(productGroups, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
