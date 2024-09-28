using AvaloniaApp.Business.Modules.UserAccess.DomainServices;
using AvaloniaApp.Business.Modules.UserAccess.DTOs;
using AvaloniaApp.Data;
using AvaloniaApp.Data.Entities;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace AvaloniaApp.Business.Modules.UserAccess.ApplicationServices;

public class UserAccessService : IUserAccessService
{
    private readonly IDataAccess _dataAccess;
    private readonly SessionValueResolver _sessionValueResolver;
    private IValidator<UserConfigDto> _userConfigValidator;
    private readonly UserAccessMapper _userAccessMapper = new();

    public UserAccessService(IDataAccess dataAccess, SessionValueResolver sessionValueResolver, IValidator<UserConfigDto> userConfigValidator)
    {
        _dataAccess = dataAccess;
        _sessionValueResolver = sessionValueResolver;
        _userConfigValidator = userConfigValidator;
    }

    public ValidationResult Authenticate(NetworkCredential networkCredentials)
    {
        EndUser? user = _dataAccess.ReadFiltered<EndUser>(u => string.Equals(networkCredentials.UserName, u.UserName), [nameof(EndUser.UserCredential)]).FirstOrDefault();
        if (user == null)
        {
            return new ValidationResult([new ValidationFailure() { ErrorMessage = "User Name not found." }]);
        }

        if (user.UserCredential == null || !Authorizer.Login(_userAccessMapper.MapToDto<UserCredentialDto>(user.UserCredential), networkCredentials.SecurePassword))
        {
            return new ValidationResult([new ValidationFailure() { ErrorMessage = "Bad credentials." }]);
        }

        _sessionValueResolver.SetSessionUserID(user.EndUserId);
        return new ValidationResult();
    }

    public ValidationResult Register(NetworkCredential networkCredentials)
    {
        if (string.IsNullOrEmpty(networkCredentials.UserName) || _dataAccess.ReadFiltered<EndUser>(eu => string.Equals(networkCredentials.UserName, eu.UserName)).Any())
        {
            return new ValidationResult([new ValidationFailure() { ErrorMessage = "User Name must be unique." }]);
        }

        EndUserDto newUser = new()
        {
            UserName = networkCredentials.UserName,
            UserCredential = Authorizer.CreateAppCredential(networkCredentials.SecurePassword),
            UserConfig = new()
        };

        EndUser userEntity = _dataAccess.Update(_userAccessMapper.MapToEntity<EndUser>(newUser));

        _sessionValueResolver.SetSessionUserID(userEntity.EndUserId);
        return new ValidationResult();
    }

    public UserConfigDto? GetSessionUserConfig()
    {
        return _sessionValueResolver.GetSessionUser(nameof(EndUserDto.UserConfig))?.UserConfig;
    }

    public async Task<ValidationResult> SaveSessionUserConfigAsync(UserConfigDto userConfig)
    {
        ValidationResult result = await _userConfigValidator.ValidateAsync(userConfig);

        if (result.IsValid)
        {
            _dataAccess.Update(_userAccessMapper.MapToEntity<UserConfig>(userConfig));
        }

        return result;
    }
}
