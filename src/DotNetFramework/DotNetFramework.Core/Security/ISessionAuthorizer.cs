using System;

namespace DotNetFramework.Core.Security
{
    public interface ISessionAuthorizer
    {
        bool Login(SessionCredential credential, string password);

        SessionCredential Register(string password);
    }
}
