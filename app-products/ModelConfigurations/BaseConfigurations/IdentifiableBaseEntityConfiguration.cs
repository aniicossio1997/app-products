using app_products.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace app_products.ModelConfigurations.BaseConfigurations
{
    internal static class IdentifiableBaseEntityConfiguration
    {
        public static void Configure<T>(EntityTypeBuilder<T> entityTypeConfiguration) where T : IdentifiableBaseEntity
        {
            entityTypeConfiguration.HasKey(ent => ent.Id);
        }
    }
}
