using app_products.Models;
using app_products.ViewModels.ClassesBase;

namespace app_products.ViewModels
{
    public class ProductViewModel:TypeBaseViewModel
    {
        public CategoryViewModel Category { get; set; }

        public string Date { get; set; }
        public int Price { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProductViewModel model &&
                   base.Equals(obj) &&
                   Id == model.Id &&
                   Name == model.Name &&
                   EqualityComparer<CategoryViewModel>.Default.Equals(Category, model.Category) &&
                   Date == model.Date &&
                   Price == model.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id, Name, Category, Date, Price);
        }
    }
}
