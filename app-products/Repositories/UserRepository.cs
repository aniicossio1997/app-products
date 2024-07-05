using app_products.Exceptions;
using app_products.ModelConfigurations;
using app_products.Models;
using app_products.Repositories.IRepositories;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace app_products.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ExistsByFilter(UserFilterViewModel filters)
        {
            return await GetQueryableByFilter(filters)
                            .AnyAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetByFilter(UserFilterViewModel filters)
        {
            return await GetQueryableByFilter(filters )
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserViewModel> GetFirstByFilter(UserFilterViewModel filters)
        {
            return await GetQueryableByFilter(filters)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<UserViewModel> Save(RegistrationViewModel entityToAdd)
        {
            //cancellationToken.ThrowIfCancellationRequested();

            var entity = _mapper.Map<User>(entityToAdd);


            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return await GetFirstByFilter(new UserFilterViewModel { Id = entity.Id });

        }

        private IQueryable<User> GetQueryableByFilter(UserFilterViewModel filters)
        {
            var query = _context.Users.AsQueryable();

            if (filters.Id.HasValue)
                query = query.Where(p => p.Id == filters.Id);

            if (filters.UserName !=null)
                query = query.Where(p => p.Username==filters.UserName);

            return query;
        }
    }
}
