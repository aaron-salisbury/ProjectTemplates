using DotNet.Business.Modules.UserAccess.DTOs;
using FluentValidation;

namespace DotNet.Business.Modules.Trading.Validators;

public class EndUserValidator : AbstractValidator<EndUserDto>
{
    public EndUserValidator()
    {
        RuleFor(dto => dto.UserConfig).SetValidator(new UserConfigValidator());
    }
}
