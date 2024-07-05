using app_products.ViewModels;
using app_products.ViewModels.ClassesBase;

namespace app_products.Services.IServices
{
    public interface IAuthService
    {
        Task<UserViewModel> SignUp(RegistrationViewModel signupData);

        Task<UserSessionViewModel> LogIn(LoginViewModel loginRequest);
    }
}
