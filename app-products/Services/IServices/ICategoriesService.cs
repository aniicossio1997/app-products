using app_products.ViewModels;
using app_products.ViewModels.Filters;

namespace app_products.Services.IServices
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryViewModel>> GetByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken=default);

    }
}
