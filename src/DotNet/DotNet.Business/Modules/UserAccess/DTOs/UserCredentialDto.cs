using System;

namespace DotNet.Business.Modules.UserAccess.DTOs
{
    public record UserCredentialDto
    {
        public int UserCredentialId { get; init; }

        public int EndUserId { get; init; }

        public required byte[] LoginSalt { get; init; }

        public required byte[] LoginHash { get; init; }

        public required int LoginWorkFactor { get; init; }
    }
}
