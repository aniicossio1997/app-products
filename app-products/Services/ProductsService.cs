using app_products.Exceptions;
using app_products.Repositories.IRepositories;
using app_products.Services.IServices;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace app_products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }
        public async Task<IEnumerable<ProductViewModel>> GetByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _productsRepository.GetByFilter(filters, cancellationToken);
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _productsRepository.Delete(id) ;
        }

        public Task<ProductViewModel> Save(ProductPostViewModel entityToAdd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _productsRepository.Save(entityToAdd);
        }

        public Task<ProductViewModel> Update(int id, ProductPutViewModel entityToEdit, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (id != entityToEdit.Id)
            {
                throw new IncorrectIdException();

            }
            return _productsRepository.Update(entityToEdit);

        }
    }
}
