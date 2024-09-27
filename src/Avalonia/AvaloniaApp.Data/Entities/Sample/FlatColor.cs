using System;

namespace AvaloniaApp.Data.Entities.Sample
{
    public record FlatColor
    {
        public required string Name { get; init; }
        public required string Hex { get; init; }
    }
}
