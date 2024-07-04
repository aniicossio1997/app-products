using app_products.ViewModels;
using app_products.ViewModels.Filters;

namespace app_products.Repositories.IRepositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductViewModel>> GetByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken);
        Task<bool> ExistsByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken = default);
        Task<ProductViewModel> GetFirstByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken);
        Task<ProductViewModel> Save(ProductPostViewModel entityToAdd, CancellationToken cancellationToken = default);

        Task<ProductViewModel> Update(ProductPutViewModel entityToEdit, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

    }
}
