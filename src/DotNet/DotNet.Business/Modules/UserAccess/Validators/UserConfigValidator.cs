using DotNet.Business.Modules.UserAccess.DTOs;
using FluentValidation;

namespace DotNet.Business.Modules.Trading.Validators
{
    public class UserConfigValidator : AbstractValidator<UserConfigDto>
    {
        public UserConfigValidator()
        {
            RuleFor(dto => dto.Email).EmailAddress();
        }
    }
}
