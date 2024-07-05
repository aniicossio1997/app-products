using app_products.ModelConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using app_products.Models;

namespace app_products.ModelConfigurations
{

    internal partial class ApplicationDbContext { public DbSet<User> Users { get; set; } = null!; }

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            NameableBaseEntityConfiguration.Configure(builder);
            builder.Property(ent => ent.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    ;
            builder.HasIndex(ent => ent.Username).IsUnique();

            builder.Property(ent => ent.Password)
                .IsRequired();

            builder.Property(ent => ent.LastName)
                .HasMaxLength(50)
                ;

        }
    }
}
