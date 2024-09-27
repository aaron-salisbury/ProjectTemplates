using System;

namespace AvaloniaApp.Business.Modules.UserAccess.DTOs;

public record EndUserDto
{
    public int EndUserId { get; init; }

    public required string UserName { get; init; }

    public required UserCredentialDto UserCredential { get; init; }

    public required UserConfigDto UserConfig { get; init; }
}
