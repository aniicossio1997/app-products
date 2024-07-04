using app_products.Models.Base;

namespace app_products.Models
{
    internal class Product : IdentifiableBaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
