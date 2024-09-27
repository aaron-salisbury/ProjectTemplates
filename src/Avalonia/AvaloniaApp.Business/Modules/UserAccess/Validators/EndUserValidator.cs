using AvaloniaApp.Business.Modules.UserAccess.DTOs;
using FluentValidation;

namespace AvaloniaApp.Business.Modules.Trading.Validators;

public class EndUserValidator : AbstractValidator<EndUserDto>
{
    public EndUserValidator()
    {
        RuleFor(dto => dto.UserConfig).SetValidator(new UserConfigValidator());
    }
}
