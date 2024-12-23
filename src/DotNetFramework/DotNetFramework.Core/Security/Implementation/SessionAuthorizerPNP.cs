﻿using DotNetFramework.Core.ExtensionHelpers;
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

        private readonly int _workFactor;

        public SessionAuthorizerPNP(HashAlgorithm hashAlgorithm = null, int registrationWorkFactor = 1000)
            : base(hashAlgorithm != null ? hashAlgorithm.GetType() : typeof(SHA256Managed), saltEnabled: true)
        {
            _workFactor = registrationWorkFactor;
        }

        public bool Login(SessionCredential credential, string password)
        {
            return CompareHash(StringExtensions.ToBytes(password), credential.Hash, credential.Salt, credential.WorkFactor);
        }

        public SessionCredential Register(string password)
        {
            byte[] salt = GenerateSalt();

            return new SessionCredential()
            {
                Salt = salt,
                Hash = CreateHash(StringExtensions.ToBytes(password), salt, _workFactor),
                WorkFactor = _workFactor
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
