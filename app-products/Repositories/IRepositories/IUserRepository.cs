using app_products.ViewModels;
using app_products.ViewModels.ClassesBase;
using app_products.ViewModels.Filters;

namespace app_products.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<UserViewModel> Save(RegistrationViewModel entityToAdd);
        Task<IEnumerable<UserViewModel>> GetByFilter(UserFilterViewModel filters);
        Task<bool> ExistsByFilter(UserFilterViewModel filters);
        Task<UserViewModel> GetFirstByFilter(UserFilterViewModel filters);

    }
}
