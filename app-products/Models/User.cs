using app_products.Models.Base;

namespace app_products.Models
{
    internal class User :NameableBaseEntity
    {
        public string LastName { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }

    }
}
