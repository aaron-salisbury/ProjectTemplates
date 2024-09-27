using AvaloniaApp.Business.Base.Extensions;
using AvaloniaApp.Business.Modules.UserAccess.DTOs;
using System.Security;
using System.Security.Cryptography;

namespace AvaloniaApp.Business.Modules.UserAccess.DomainServices;

// ref: https://www.mking.net/blog/password-security-best-practices-with-examples-in-csharp

internal static class Authorizer
{
    private const int SALT_LENGTH = 32;
    private const int NEW_USER_WORK_FACTOR = 10000;

    public static bool Login(UserCredentialDto credential, SecureString password)
    {
        return credential.LoginHash.EqualsByteArray(GenerateHash(
            password.ToBytes(),
            credential.LoginSalt,
            credential.LoginWorkFactor));
    }

    public static UserCredentialDto CreateAppCredential(SecureString password)
    {
        byte[] loginSalt = GenerateSalt();

        return new UserCredentialDto()
        {
            LoginSalt = loginSalt,
            LoginHash = GenerateHash(password.ToBytes(), loginSalt, NEW_USER_WORK_FACTOR),
            LoginWorkFactor = NEW_USER_WORK_FACTOR
        };
    }

    private static byte[] GenerateSalt()
    {
        var bytes = new byte[SALT_LENGTH];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        return bytes;
    }

    private static byte[] GenerateHash(byte[] password, byte[] salt, int workFactor)
    {
        using (Rfc2898DeriveBytes deriveBytes = new(password, salt, workFactor, HashAlgorithmName.SHA256))
        {
            return deriveBytes.GetBytes(SALT_LENGTH);
        }
    }
}
