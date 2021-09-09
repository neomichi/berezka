using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Berezka.Data.Service
{
    public class AesHelper : ICryptoHelper
    {
        private readonly IConfiguration _configuration;
        public AesHelper(IConfiguration configuration)
        {
            _configuration = configuration;
  
        }


        public string EncryptData(Guid UserId, string fullstringdate, string securityKey)
        {
            var result = "";
            var secret = $"{UserId},{fullstringdate},{securityKey}";
            try
            {               
                result = EncryptString(securityKey, secret);
                
            } catch 
            {
                return result;
            }
            return result;
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public (Guid UserId, DateTime fulldatetime, string securityKey) DecryptData(string cipherText)
        {
            var securityKey = _configuration.GetSection("Jwt")["SecurityKey"];
            var result = DecryptString(securityKey, cipherText);
            var array = result.Split(',');
            return (array[0].ToGuid(), array[1].ParceFullToDateTime(), array[2]);
        }
    }
}
