using DotNet.Business.Modules.UserAccess.DTOs;
using FluentValidation.Results;
using System.Net;

namespace DotNet.Business.Modules.UserAccess.ApplicationServices;

public interface IUserAccessService
{
    ValidationResult Authenticate(NetworkCredential networkCredentials);

    ValidationResult Register(NetworkCredential networkCredentials);

    UserConfigDto? GetSessionUserConfig();

    Task<ValidationResult> SaveSessionUserConfigAsync(UserConfigDto userConfig);
}
