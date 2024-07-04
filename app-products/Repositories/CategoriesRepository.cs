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
    internal class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await GetQueryableByFilter(filters, cancellationToken)
                            .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await GetQueryableByFilter(filters, cancellationToken)
                            .AnyAsync(cancellationToken);
        }

        private IQueryable<Category> GetQueryableByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = _context.Categories.AsQueryable();

            if (filters.Id.HasValue)
                query = query.Where(p => p.Id == filters.Id);


            return query;
        }

        public async Task<CategoryViewModel> GetFirstByFilter(CategoryFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await GetQueryableByFilter(filters, cancellationToken)
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
