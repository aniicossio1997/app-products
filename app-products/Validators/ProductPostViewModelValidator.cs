using app_products.Enums;
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

            RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage($"{MessageError.NumberIsGreter} a 0")
            .LessThanOrEqualTo(1000000).WithMessage($"{MessageError.NumberIsLower} a 1000000");
             
            RuleFor(p => p.Name)
                        .NotEmpty().WithMessage(MessageError.NameIsRequired)
                        .MaximumLength(50).WithMessage($"{MessageError.MaxLength}, el maximo es 50");

        }
    }
}
