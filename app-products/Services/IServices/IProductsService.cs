using app_products.ViewModels.Filters;
using app_products.ViewModels;

namespace app_products.Services.IServices
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> GetByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken = default);

        Task<ProductViewModel> Save(ProductPostViewModel entityToAdd, CancellationToken cancellationToken = default);

        Task<ProductViewModel> Update(int id, ProductPutViewModel entityToAdd, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        Task<ProductViewModel> GetFirstByFilter(int id, CancellationToken cancellationToken);


    }
}
