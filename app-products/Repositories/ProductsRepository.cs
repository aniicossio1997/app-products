using app_products.Exceptions;
using app_products.ModelConfigurations;
using app_products.Models;
using app_products.Repositories.IRepositories;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace app_products.Repositories
{
    internal class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await GetQueryableByFilter(filters, cancellationToken)
                            .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await GetQueryableByFilter(filters, cancellationToken)
                            .AnyAsync(cancellationToken);
        }

        private IQueryable<Product> GetQueryableByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = _context.Products.AsQueryable();

            if (filters.Id.HasValue)
                query = query.Where(p => p.Id == filters.Id);

            return query;
        }

        public async Task<ProductViewModel> GetFirstByFilter(ProductFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await GetQueryableByFilter(filters, cancellationToken)
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        public async Task<ProductViewModel> Save(ProductPostViewModel entityToAdd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = _mapper.Map<Product>(entityToAdd);
            entity.Category = _context.Categories.Where(p => p.Id == entityToAdd.CategoryId).FirstOrDefault();

            _context.Products.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return await GetFirstByFilter(new ProductFilterViewModel { Id = entity.Id }, cancellationToken);
        }

        public async Task<ProductViewModel> Update(ProductPutViewModel entityToEdit, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entityDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == entityToEdit.Id, cancellationToken);
            if (entityDb == null) throw new EntityToEditNotFoundException();
            entityDb = _mapper.Map(entityToEdit, entityDb);
            _context.Update(entityDb);
            await _context.SaveChangesAsync(cancellationToken);

            return await GetFirstByFilter(new ProductFilterViewModel { Id = entityToEdit.Id }, cancellationToken);
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entityDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (entityDb == null) return false;

            _context.Products.Remove(entityDb);

            cancellationToken.ThrowIfCancellationRequested();
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
