using app_products.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace app_products.ModelConfigurations
{
    internal partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0])));


            CategoryConfiguration.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
