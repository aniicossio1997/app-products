namespace app_products.ViewModels
{
    public class ProductPutViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
