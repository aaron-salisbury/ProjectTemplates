using AvaloniaApp.Business.Modules.UserAccess.DTOs;
using FluentValidation;

namespace AvaloniaApp.Business.Modules.Trading.Validators
{
    public class UserConfigValidator : AbstractValidator<UserConfigDto>
    {
        public UserConfigValidator()
        {
            RuleFor(dto => dto.Email).EmailAddress();
        }
    }
}
