using System;

namespace Build.DTOs;

public record ReleaseProject
{
    public required string Name { get; init; }

    public required string DirectoryPathAbsolute { get; init; }

    public required string CsprojFilePathAbsolute { get; init; }

    public required string OutputDirectoryPathAbsolute { get; init; }

    public required bool IsSdkStyleProject { get; init; }
}
