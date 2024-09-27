using AvaloniaApp.Business.Modules.UserAccess.DTOs;
using AvaloniaApp.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace AvaloniaApp.Business.Modules.UserAccess;

[Mapper]
internal partial class UserAccessMapper
{
    internal partial TTarget MapToDto<TTarget>(object source);
    internal partial TTarget MapToEntity<TTarget>(object source);

    private partial EndUserDto MapToDto(EndUser source);
    private partial EndUser MapToEntity(EndUserDto source);

    private partial UserCredentialDto MapToDto(UserCredential source);
    private partial UserCredential MapToEntity(UserCredentialDto source);

    private partial UserConfigDto MapToDto(UserConfig source);
    private partial UserConfig MapToEntity(UserConfigDto source);
}
