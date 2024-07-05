using app_products.ViewModels.ClassesBase;

namespace app_products.ViewModels
{
    public class UserViewModel :TypeBaseViewModel
    {
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; }

    }
}
