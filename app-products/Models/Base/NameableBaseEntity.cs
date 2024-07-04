namespace app_products.Models.Base
{
    public class NameableBaseEntity : IdentifiableBaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
