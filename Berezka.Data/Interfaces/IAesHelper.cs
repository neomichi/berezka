using System;

namespace Berezka.Data.Service
{
    public interface ICryptoHelper
    {
        (Guid UserId, DateTime fulldatetime, string securityKey) DecryptData(string cipherText);
        string EncryptData(Guid UserId, string fullstringdate, string securityKey);
    }
}