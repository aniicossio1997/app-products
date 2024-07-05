using app_products.Enums;
using app_products.Exceptions;
using app_products.Models;
using app_products.Repositories.IRepositories;
using app_products.Services.IServices;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using System.Threading;
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

        public async Task<ProductViewModel> GetFirstByFilter(int id, CancellationToken cancellationToken)
        {
            return await _productsRepository.GetFirstByFilter(new ProductFilterViewModel { Id=id},cancellationToken);
        }
        public async Task<IEnumerable<ProductViewModel>> GetByBudgetPrice(ProductFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (filters.BudgetPrice==0 && filters.BudgetPrice==null)
            {
                return (await _productsRepository.GetByFilter(new ProductFilterViewModel { }, cancellationToken));
            }
            // Obtener todos los productos que cumplen con los filtros básicos
            var productsList = (await _productsRepository.GetByFilter(new ProductFilterViewModel { }, cancellationToken)).ToList();
            // Filtrar y ordenar los productos por categoría y precio
            var groupedProducts = productsList
                .Where(p => p.Price < filters.BudgetPrice) // Filtrar por precio dentro del presupuesto
                .GroupBy(p => p.Category.Id) // Agrupar por categoría
                .Select(g => g.OrderByDescending(p => p.Price).FirstOrDefault()) // Seleccionar el más caro por categoría
                .ToList();

            // Validar si se encontró al menos un producto por categoría
            if (groupedProducts.Count < 2)
            {
                throw new EntityNotFoundException(MessageError.NoBudgetFound);
            }

            // Seleccionar los dos productos con la menor diferencia respecto al presupuesto

            return await FindBestCombinationV1(filters.BudgetPrice, cancellationToken);



        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _productsRepository.Delete(id) ;
        }

        public async Task<ProductViewModel> Save(ProductPostViewModel entityToAdd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _productsRepository.Save(entityToAdd);
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

       

        private async Task<IEnumerable<ProductViewModel>> FindBestCombinationV1( int budgetPrice, CancellationToken cancellationToken)
        {
            var bestCombination = new List<ProductViewModel>();
            var minDifference = int.MaxValue;
            var category1Products = (await _productsRepository.GetByFilter(new ProductFilterViewModel { 
                CategoryId=(int)CategoryTypeEnum.One}, cancellationToken))
                .OrderByDescending(p => p.Price)
                .ToList();

            var category2Products = (await _productsRepository.GetByFilter(new ProductFilterViewModel {
                CategoryId = (int)CategoryTypeEnum.Two }, cancellationToken))
                .OrderByDescending(p => p.Price)
                .ToList();

            // Generar todas las combinaciones de productos de ambas categorías
            foreach (var product1 in category1Products)
            {
                foreach (var product2 in category2Products)
                {
                    var totalPrice = product1.Price + product2.Price;
                    var difference = budgetPrice - totalPrice;

                    // Verificar si es una combinación válida y optimizar la diferencia mínima
                    if (totalPrice <= budgetPrice && difference < minDifference)
                    {
                        bestCombination.Clear();
                        bestCombination.Add(product1);
                        bestCombination.Add(product2);
                        minDifference = difference;
                    }
                }
            }

            // Validar si se encontró una combinación válida
            if (bestCombination.Count == 0)
            {
                throw new EntityNotFoundException(MessageError.NoBudgetFound);
            }

            return bestCombination;
        }

        private async Task<IEnumerable<ProductViewModel>> FindBestCombinationV2(int budgetPrice, CancellationToken cancellationToken)
        {
            // Obtener todos los productos de todas las categorías y ordenarlos por precio descendente
            var allProducts = await _productsRepository.GetByFilter(new ProductFilterViewModel { },cancellationToken);


            // Inicializar la mejor combinación y la mínima diferencia
            List<ProductViewModel> bestCombination = new List<ProductViewModel>();
            int minDifference = int.MaxValue;

            var combinations = from p1 in allProducts
                               from p2 in allProducts
                               where p1.Category.Id != p2.Category.Id // Productos de diferentes categorías
                               let totalPrice = p1.Price + p2.Price
                               let difference = Math.Abs(totalPrice - budgetPrice)
                               where totalPrice <= budgetPrice
                               orderby difference
                               select new { Product1 = p1, Product2 = p2, TotalPrice = totalPrice, Difference = difference };

            var bestMatch = combinations.FirstOrDefault();

            if (bestMatch != null)
            {
                bestCombination.Add(bestMatch.Product1);
                bestCombination.Add(bestMatch.Product2);
            }
            else
            {
                throw new EntityNotFoundException(MessageError.NoBudgetFound);
            }

            return bestCombination;
        }


    }
}
