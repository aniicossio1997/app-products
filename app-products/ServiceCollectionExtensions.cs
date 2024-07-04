using System.Reflection;

namespace app_products
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, string interfaceNamespace, string implementationNamespace)
        {
            var interfaceAssembly = Assembly.GetExecutingAssembly();
            var implementationAssembly = Assembly.GetExecutingAssembly();

            var interfaces = interfaceAssembly.GetTypes()
                .Where(t => t.IsInterface && t.Namespace == interfaceNamespace)
                .ToList();

            var implementations = implementationAssembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace == implementationNamespace)
                .ToList();

            foreach (var interfaceType in interfaces)
            {
                var implementationType = implementations
                    .FirstOrDefault(t => t.GetInterfaces().Contains(interfaceType));

                if (implementationType != null)
                {
                    services.AddTransient(interfaceType, implementationType);
                }
            }
        }
    }
}
