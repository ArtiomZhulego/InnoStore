using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LanguageConfigurator : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasIndex(x => x.ISOCode).IsUnique();

        builder.Property(x => x.ISOCode)
            .IsRequired()
            .HasMaxLength(2);

        builder.HasData(
            new Language { Id = Guid.Parse("130c70f4-bccd-4452-94c3-696788aa66ee"), ISOCode = "en" },
            new Language { Id = Guid.Parse("3d3365ea-797a-4edc-b1b2-524723b911a9"), ISOCode = "ru" }
        );
    }
}
