using app_products.ViewModels;
using app_products.ViewModels.Filters;

namespace app_products.Repositories.IRepositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<CategoryViewModel>> GetByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken);
        Task<bool> ExistsByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken = default);
        Task<CategoryViewModel> GetFirstByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken);
    }
}
