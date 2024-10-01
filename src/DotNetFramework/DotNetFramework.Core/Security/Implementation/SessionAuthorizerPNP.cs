﻿using DotNetFramework.Core.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.Security.Cryptography;

namespace DotNetFramework.Core.Security
{
    /// <summary>
    /// Patterns & Practices implementation of ISessionAuthorizer.
    /// </summary>
    public class SessionAuthorizerPNP : HashAlgorithmProvider, ISessionAuthorizer
    {
        private const int SALT_LENGTH = HashAlgorithmProvider.SaltLength;
        private const int WORK_FACTOR_V1 = 1000;

        public SessionAuthorizerPNP() : base(typeof(SHA256), saltEnabled: true) { }

        public bool Login(SessionCredential credential, string password)
        {
            return CompareHash(password.ToBytes(), credential.Hash, credential.Salt, credential.WorkFactor);
        }

        public SessionCredential Register(string password)
        {
            byte[] salt = GenerateSalt();

            return new SessionCredential()
            {
                Salt = salt,
                Hash = CreateHash(password.ToBytes(), salt, WORK_FACTOR_V1),
                WorkFactor = WORK_FACTOR_V1
            };
        }

        private byte[] CreateHash(byte[] plainText, byte[] salt, int workFactor)
        {
            workFactor = workFactor > 0 ? workFactor : 1;
            byte[] runningHash = plainText;

            for (int i = 0; i < workFactor; i++)
            {
                runningHash = CreateHashWithSalt(runningHash, salt);
            }

            return runningHash;
        }

        private bool CompareHash(byte[] plainText, byte[] comparatorHash, byte[] salt, int workFactor)
        {
            byte[] comparandHash = CreateHash(plainText, salt, workFactor);

            return CryptographyUtility.CompareBytes(comparandHash, comparatorHash);
        }

        private static byte[] GenerateSalt()
        {
            return CryptographyUtility.GetRandomBytes(SALT_LENGTH);
        }
    }
}
