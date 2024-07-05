using app_products.Enums;
using app_products.Repositories.IRepositories;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using FluentValidation;

namespace app_products.Validators
{
    public class ProductPutViewModelValidator : AbstractValidator<ProductPutViewModel>
    {
        public ProductPutViewModelValidator(
            IProductsRepository _productsRepository,
            ICategoriesRepository _categoriesRepository

        )
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(p => p.Id)
            .NotNull()
            .Must(p => _productsRepository.ExistsByFilter(new ProductFilterViewModel { Id = p }).Result);

            RuleFor(p => p.CategoryId)
                    .NotNull()
                    .Must(p => _categoriesRepository.ExistsByFilter(new CategoryFilterViewModel { Id = p }).Result);

            RuleFor(e => e.Price)
            .GreaterThan(0).WithMessage($"{MessageError.NumberIsGreter} a 0")
            .LessThanOrEqualTo(1000000).WithMessage($"{MessageError.NumberIsLower} a 1000000");

            RuleFor(e => e.Name)
                        .NotEmpty().WithMessage(MessageError.NameIsRequired)
                        .MaximumLength(50).WithMessage($"{MessageError.MaxLength}, el maximo es 50");

            RuleFor(e => e.Date)
                        .NotEmpty().WithMessage(MessageError.required);

        }
    }
}
