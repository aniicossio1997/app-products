using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using app_products.Models;
using app_products.ModelConfigurations.BaseConfigurations;

namespace app_products.ModelConfigurations
{

    internal partial class ApplicationDbContext { public DbSet<Category> Categories { get; set; } = null!; }

    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");
            NameableBaseEntityConfiguration.Configure(builder);



        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new Category
                {
                    Id = 1,
                    Name = "Indumentaria",
                    
                });

            modelBuilder.Entity<Category>()
                .HasData(new Category
                {
                    Id = 2,
                    Name = "Accesorios",

                });

        }
    }
}
