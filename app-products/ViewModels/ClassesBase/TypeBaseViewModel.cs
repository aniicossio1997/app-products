namespace app_products.ViewModels.ClassesBase
{
    public class TypeBaseViewModel : IdentificableViewModel
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TypeBaseViewModel model &&
                   base.Equals(obj) &&
                   Name == model.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name);
        }
    }
}
