using app_products.Models.Base;

namespace app_products.Models
{
    internal class Product : NameableBaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime Date { get; set; }
        public int Price { get; set; }
    }
}
