using app_products.Enums;
using app_products.Repositories.IRepositories;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using FluentValidation;

namespace app_products.Validators
{
    public class ProductFilterViewModelValidator : AbstractValidator<ProductFilterViewModel>
    {
        public ProductFilterViewModelValidator()
                {

            RuleFor(e => e.BudgetPrice)
            .GreaterThan(0).WithMessage($"{MessageError.NumberIsGreter} a 0")
            .LessThanOrEqualTo(1000000).WithMessage($"{MessageError.NumberIsLower} a 1000000");
        }
    }

}
