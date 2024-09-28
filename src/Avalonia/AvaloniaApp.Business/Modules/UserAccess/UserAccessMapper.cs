using DotNet.Business.Modules.UserAccess.DTOs;
using DotNet.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace DotNet.Business.Modules.UserAccess;

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
