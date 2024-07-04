using app_products.Repositories.IRepositories;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using FluentValidation;

namespace app_products.Validators
{
    public class ProductPostViewModelValidator : AbstractValidator<ProductPostViewModel>
    {
        public ProductPostViewModelValidator(
            ICategoriesRepository _categoriesRepository

        )
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(p => p.CategoryId)
                    .NotNull()
                    .Must(p => _categoriesRepository.ExistsByFilter(new CategoryFilterViewModel { Id = p }).Result);

            RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.")
            .LessThanOrEqualTo(1000000).WithMessage("El precio no debe superar 1 millón.");

                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("El nombre es requerido.")
                        .MaximumLength(50).WithMessage("El nombre no debe tener más de 50 caracteres.");

        }
    }
}
