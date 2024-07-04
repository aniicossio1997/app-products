using app_products.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace app_products.ModelConfigurations.BaseConfigurations
{
    internal static class NameableBaseEntityConfiguration
    {
        public static void Configure<T>(EntityTypeBuilder<T> entityTypeConfiguration) where T : NameableBaseEntity
        {
            IdentifiableBaseEntityConfiguration.Configure(entityTypeConfiguration);
            entityTypeConfiguration
                .Property(ent => ent.Name)
                .IsRequired()
                .HasMaxLength(50);
        }


    }
}
