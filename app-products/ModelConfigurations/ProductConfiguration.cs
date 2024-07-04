using app_products.ModelConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using app_products.Models;

namespace app_products.ModelConfigurations
{

    internal partial class ApplicationDbContext { public DbSet<Product> Products { get; set; } = null!; }

    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
            NameableBaseEntityConfiguration.Configure(builder);


        }
    }
}
