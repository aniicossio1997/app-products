using app_products.Enums;
using app_products.Repositories.IRepositories;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using FluentValidation;

namespace app_products.Validators
{
    public class ResgitrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public ResgitrationViewModelValidator(
            IUserRepository _userRepository

        )
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(p => p.Username)
                    .NotNull()
                    .Must(p => !_userRepository.ExistsByFilter(new UserFilterViewModel {UserName=p }).Result);

            RuleFor(p => p.Password)
                       .NotEmpty().WithMessage("La contraseña es requerida.")
                       .Length(2, 50).WithMessage("La contraseña debe tener entre 2 y 50 caracteres.");

            RuleFor(p => p.Name)
                        .NotEmpty().WithMessage(MessageError.NameIsRequired)
                        .MaximumLength(50).WithMessage($"{MessageError.MaxLength}, el maximo es 50");

            RuleFor(p => p.LastName)
                        .MaximumLength(50).WithMessage($"{MessageError.MaxLength}, el maximo es 50");

        }
    }
}
