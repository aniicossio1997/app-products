using app_products.Models;
using app_products.ViewModels.ClassesBase;

namespace app_products.ViewModels
{
    public class ProductViewModel:TypeBaseViewModel
    {
        public CategoryViewModel Category { get; set; }

        public string Date { get; set; }
        public int Price { get; set; }
    }
}
