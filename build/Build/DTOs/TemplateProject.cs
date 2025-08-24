using System.Collections.Generic;

namespace Build.DTOs;

public record TemplateProject : ReleaseProject
{
    public required bool IsApplication { get; init; }

    public required string FriendlyName { get; init; }

    public required string Description { get; init; }

    public required HashSet<string> ProjectNamesReferenced { get; init; }
}
