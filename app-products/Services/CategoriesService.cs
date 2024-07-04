using app_products.Repositories.IRepositories;
using app_products.Services.IServices;
using app_products.ViewModels;
using app_products.ViewModels.Filters;

namespace app_products.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository  _categoryRepository;

        public CategoriesService(ICategoriesRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _categoryRepository.GetByFilter(filters, cancellationToken);
        }
    }
}
